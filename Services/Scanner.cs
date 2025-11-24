namespace PortScanner.Services;

using PortScanner.Models;
using PortScanner.Utils;
using System.Collections.Concurrent;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public sealed class Scanner
{
    public event Action<ScanRecord>? Record;
    public event Action<string>? Log;
    public event Action? Completed;

    private BlockingCollection<IPAddress>? queue;
    private CancellationTokenSource? cts;
    private List<Task>? workers;
    private SemaphoreSlim? limiter;
    private bool largeRangeMode = false;
    private long offlineCounter = 0;

    private readonly string startIp;
    private readonly string endIp;
    private readonly int workerCount;
    private readonly bool enableDns;
    private readonly bool enablePortScan;
    private readonly List<int> ports;

    public Scanner(string startIp, string endIp, int workerCount, bool enableDns, bool enablePortScan, IEnumerable<int> ports)
    {
        this.startIp = startIp;
        this.endIp = endIp;
        this.workerCount = workerCount;
        this.enableDns = enableDns;
        this.enablePortScan = enablePortScan;
        this.ports = ports.ToList();
    }

    public void Start()
    {
        if (queue != null) return;
        cts = new CancellationTokenSource();
        queue = new BlockingCollection<IPAddress>(10000);
        workers = new List<Task>();
        limiter = new SemaphoreSlim(workerCount);
        var token = cts.Token;
        Task.Run(() => ProduceAddresses(token));
        for (int i = 0; i < workerCount; i++)
        {
            int workerId = i + 1;
            workers.Add(Task.Run(() => ConsumeAndScan(workerId, token)));
        }
    }

    public void Stop()
    {
        try { cts?.Cancel(); } catch { }
        try { queue?.CompleteAdding(); } catch { }
    }

    private async Task ProduceAddresses(CancellationToken token)
    {
        if (!IpUtils.TryParseIPv4(startIp, out var start) || !IpUtils.TryParseIPv4(endIp, out var end))
        {
            Log?.Invoke($"[{DateTime.Now:HH:mm:ss}] Input: Neplatný rozsah\n");
            queue!.CompleteAdding();
            return;
        }
        if (start > end)
        {
            var t = start; start = end; end = t;
        }
        ulong total = end - start + 1UL;
        largeRangeMode = total > 1_000_000UL;
        offlineCounter = 0;
        for (ulong i = 0; i < total; i++)
        {
            if (token.IsCancellationRequested) break;
            var ip = IpUtils.FromUInt32((uint)(start + i));
            try { queue!.Add(ip, token); }
            catch { break; }
        }
        queue!.CompleteAdding();
    }

    private async Task ConsumeAndScan(int workerId, CancellationToken token)
    {
        foreach (var ip in queue!.GetConsumingEnumerable(token))
        {
            if (token.IsCancellationRequested) break;
            Log?.Invoke($"[{DateTime.Now:HH:mm:ss}] W{workerId}: Scanning {ip}\n");
            await limiter!.WaitAsync(token);
            try
            {
                var ping = new Ping();
                PingReply? reply = null;
                try { reply = await ping.SendPingAsync(ip, 1000); }
                catch { reply = null; }
                string status;
                int? rtt = null;
                if (reply == null) status = "Error";
                else if (reply.Status == IPStatus.Success) { status = "Online"; rtt = (int)reply.RoundtripTime; }
                else if (reply.Status == IPStatus.TimedOut) status = "Timeout";
                else status = "Offline";
                string? hostname = null;
                if (enableDns && status == "Online")
                {
                    try
                    {
                        var entry = await Dns.GetHostEntryAsync(ip);
                        hostname = entry.HostName;
                    }
                    catch { hostname = null; }
                }
                if (enablePortScan)
                {
                    var openPorts = new List<int>();
                    foreach (var p in ports)
                    {
                        if (await IsPortOpen(ip.ToString(), p, 500, token))
                            openPorts.Add(p);
                    }
                    if (openPorts.Count > 0)
                        hostname = string.IsNullOrEmpty(hostname) ? $"Ports: {string.Join(',', openPorts)}" : $"{hostname} | Ports: {string.Join(',', openPorts)}";
                }
                if (largeRangeMode && status == "Offline")
                {
                    Interlocked.Increment(ref offlineCounter);
                }
                else
                {
                    Record?.Invoke(new ScanRecord(ip.ToString(), status, rtt, hostname));
                }
                if (rtt.HasValue) Log?.Invoke($"[{DateTime.Now:HH:mm:ss}] W{workerId}: Ping OK ({rtt.Value} ms)\n");
                else Log?.Invoke($"[{DateTime.Now:HH:mm:ss}] W{workerId}: {status}\n");
            }
            finally
            {
                try { limiter!.Release(); } catch { }
            }
        }
        if (workers != null && workers.All(w => w.IsCompleted))
        {
            if (largeRangeMode && offlineCounter > 0)
                Log?.Invoke($"[{DateTime.Now:HH:mm:ss}] Info: Neuloženo {offlineCounter} Offline záznamů (large-range režim)\n");
            Completed?.Invoke();
        }
    }

    private static async Task<bool> IsPortOpen(string host, int port, int timeoutMs, CancellationToken token)
    {
        try
        {
            using var client = new TcpClient();
            var connectTask = client.ConnectAsync(host, port);
            var delay = Task.Delay(timeoutMs, token);
            var completed = await Task.WhenAny(connectTask, delay);
            if (completed == connectTask && client.Connected)
                return true;
        }
        catch { }
        return false;
    }
}
