namespace PortScanner.Services;

using PortScanner.Models;
using PortScanner.Utils;
using System.Collections.Concurrent;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Diagnostics;

/// <summary>
/// Vysoce paralelní LAN skener s producent–konsument architekturou.
/// Publikuje výsledky přes eventy a podporuje DNS a TCP port scan.
/// </summary>
public sealed class Scanner
{
    /// <summary>
    /// Event s každým nalezeným výsledkem skenu.
    /// </summary>
    public event Action<ScanRecord>? Record;

    /// <summary>
    /// Event s logovacím řádkem.
    /// </summary>
    public event Action<string>? Log;

    /// <summary>
    /// Event po dokončení skenu (po ukončení všech workerů).
    /// </summary>
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

    /// <summary>
    /// Vytvoří skener s daným rozsahem, paralelismem a volbami.
    /// </summary>
    /// <param name="startIp">Počáteční IPv4 adresa.</param>
    /// <param name="endIp">Koncová IPv4 adresa.</param>
    /// <param name="workerCount">Počet paralelních workerů.</param>
    /// <param name="enableDns">Zda provádět DNS lookup.</param>
    /// <param name="enablePortScan">Zda provádět TCP port scan.</param>
    /// <param name="ports">Seznam portů pro scan.</param>
    public Scanner(string startIp, string endIp, int workerCount, bool enableDns)
    {
        this.startIp = startIp;
        this.endIp = endIp;
        this.workerCount = workerCount;
        this.enableDns = enableDns;
    }

    /// <summary>
    /// Spustí sken – inicializuje frontu a spuštění producenta i workerů.
    /// </summary>
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

    /// <summary>
    /// Zastaví sken – vyvolá cancel a uzavře frontu.
    /// </summary>
    public void Stop()
    {
        try { cts?.Cancel(); } catch { }
        try { queue?.CompleteAdding(); } catch { }
    }

    /// <summary>
    /// Producent: generuje IPv4 adresy z rozsahu a vkládá je do <see cref="BlockingCollection{IPAddress}"/>.
    /// </summary>
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

    /// <summary>
    /// Konzument: čte IP z fronty, provádí Ping/DNS/Ports a publikuje výsledky.
    /// </summary>
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
                string? mac = null;
                if (status == "Online" && enableDns)
                {
                    hostname = await ResolveHostnameAsync(ip, token);
                }
                if (status == "Online")
                {
                    mac = TryGetMac(ip);
                }
                
                if (largeRangeMode && status == "Offline")
                {
                    Interlocked.Increment(ref offlineCounter);
                }
                else
                {
                    Record?.Invoke(new ScanRecord(ip.ToString(), status, rtt, hostname, mac));
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

    /// <summary>
    /// Testuje dostupnost TCP portu pomocí Connect s timeoutem.
    /// </summary>
    

    private static async Task<string?> ResolveHostnameAsync(IPAddress ip, CancellationToken token)
    {
        try
        {
            var entry = await Dns.GetHostEntryAsync(ip);
            if (!string.IsNullOrWhiteSpace(entry?.HostName))
                return entry!.HostName;
        }
        catch { }
        return await ResolveNetbiosNameAsync(ip, token);
    }

    private static async Task<string?> ResolveNetbiosNameAsync(IPAddress ip, CancellationToken token)
    {
        try
        {
            if (ip.AddressFamily != AddressFamily.InterNetwork) return null;
            using var p = new Process();
            p.StartInfo.FileName = "nbtstat";
            p.StartInfo.Arguments = $"-A {ip}";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            var linked = CancellationTokenSource.CreateLinkedTokenSource(token);
            var timeoutTask = Task.Delay(1500, linked.Token);
            var readTask = p.StandardOutput.ReadToEndAsync();
            var completed = await Task.WhenAny(readTask, timeoutTask);
            if (completed != readTask)
            {
                try { if (!p.HasExited) p.Kill(); } catch { }
                return null;
            }
            var output = readTask.Result;
            foreach (var raw in output.Split('\n'))
            {
                var line = raw.Trim();
                if (line.Length == 0) continue;
                if (line.IndexOf("<00>", StringComparison.Ordinal) >= 0 && line.IndexOf("GROUP", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0)
                    {
                        var name = parts[0].Trim();
                        if (!string.IsNullOrWhiteSpace(name)) return name;
                    }
                }
            }
        }
        catch { }
        return null;
    }

    private static string? TryGetMac(IPAddress ip)
    {
        try
        {
            if (ip.AddressFamily != AddressFamily.InterNetwork) return null;
            var addrBytes = ip.GetAddressBytes();
            int destIp = BitConverter.ToInt32(addrBytes, 0);
            var macBytes = new byte[6];
            int len = macBytes.Length;
            int result = SendARP(destIp, 0, macBytes, ref len);
            if (result != 0 || len <= 0) return null;
            return string.Join(":", macBytes.Take(len).Select(b => b.ToString("X2")));
        }
        catch { return null; }
    }

    [DllImport("iphlpapi.dll", ExactSpelling = true)]
    private static extern int SendARP(int destIp, int srcIp, byte[] pMacAddr, ref int phyAddrLen);
}
