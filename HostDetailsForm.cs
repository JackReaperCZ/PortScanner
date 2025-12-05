namespace PortScanner;

using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Collections.Concurrent;
using System.Windows.Forms;

/// <summary>
/// Detailní formulář hosta: zobrazuje identitu (IP/Hostname/MAC), skenuje služby v rozsahu portů a loguje průběh.
/// </summary>
public partial class HostDetailsForm : Form
{
    private readonly string ip;
    private readonly string? hostname;
    private readonly string? mac;
    private CancellationTokenSource? cts;
    private List<ServiceResult> all = new List<ServiceResult>();
    private readonly ConcurrentQueue<string> pendingLogs = new ConcurrentQueue<string>();
    private readonly ConcurrentQueue<ServiceResult> pendingResults = new ConcurrentQueue<ServiceResult>();
    private System.Windows.Forms.Timer? uiTimer;
    private volatile bool isClosing = false;

    /// <summary>
    /// Vytvoří detailní formulář pro daný host a nastaví minimální velikost na počáteční.
    /// </summary>
    /// <param name="ip">IP adresa hosta</param>
    /// <param name="hostname">DNS hostname (pokud je znám)</param>
    /// <param name="mac">MAC adresa (pokud je známa)</param>
    public HostDetailsForm(string ip, string? hostname, string? mac)
    {
        this.ip = ip; this.hostname = hostname; this.mac = mac;
        InitializeComponent();
        MinimumSize = Size;
        txtIp.Text = ip;
        txtHost.Text = hostname ?? string.Empty;
        txtMac.Text = mac ?? string.Empty;
        cmbStatus.SelectedIndex = 0;
        uiTimer = new System.Windows.Forms.Timer();
        uiTimer.Interval = 100;
        uiTimer.Tick += UiTimer_Tick;
        uiTimer.Start();
    }

    /// <summary>
    /// Spustí paralelní sken vybraných služeb v definovaném rozsahu portů.
    /// </summary>
    private async void btnStart_Click(object? sender, EventArgs e)
    {
        btnStart.Enabled = false; btnStop.Enabled = true;
        cts = new CancellationTokenSource();
        all.Clear();
        lvServices.Items.Clear();
        int start = (int)numPortStart.Value;
        int end = (int)numPortEnd.Value;
        var tasks = new List<Task>();
        const int MaxWorkers = 50;
        var sem = new SemaphoreSlim(MaxWorkers);
        for (int port = start; port <= end; port++)
        {
            if (chkHttp.Checked)
            {
                await sem.WaitAsync(cts!.Token);
                var p = port;
                tasks.Add(Task.Run(async () =>
                {
                    try { Log($"Worker: HTTP scan {ip}:{p}"); await ScanHttp($"HTTP {p}", p, cts!.Token); }
                    finally { sem.Release(); }
                }, cts!.Token));
            }
            if (chkHttps.Checked)
            {
                await sem.WaitAsync(cts!.Token);
                var p = port;
                tasks.Add(Task.Run(async () =>
                {
                    try { Log($"Worker: HTTPS scan {ip}:{p}"); await ScanHttp($"HTTPS {p}", p, cts!.Token, true); }
                    finally { sem.Release(); }
                }, cts!.Token));
            }
            if (chkFtp.Checked)
            {
                await sem.WaitAsync(cts!.Token);
                var p = port;
                tasks.Add(Task.Run(async () =>
                {
                    try { Log($"Worker: FTP scan {ip}:{p}"); await ScanFtp(p, cts!.Token); }
                    finally { sem.Release(); }
                }, cts!.Token));
            }
        }
        try { await Task.WhenAll(tasks); } catch { }
        if (isClosing || IsDisposed || !IsHandleCreated) return;
        ApplyFilter();
        btnStart.Enabled = true; btnStop.Enabled = false;
    }

    /// <summary>
    /// Zastaví probíhající sken pomocí Cancel a obnoví UI.
    /// </summary>
    private void btnStop_Click(object? sender, EventArgs e)
    {
        try { cts?.Cancel(); } catch { }
        btnStart.Enabled = true; btnStop.Enabled = false;
    }

    private void cmbStatus_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ApplyFilter();
    }

    /// <summary>
    /// Aplikuje aktuální stavový filtr na výsledky skenu a obnoví zobrazení.
    /// </summary>
    private void ApplyFilter()
    {
        if (isClosing || IsDisposed || !IsHandleCreated) return;
        var filter = cmbStatus.SelectedItem?.ToString() ?? "All";
        lvServices.Items.Clear();
        foreach (var r in all)
        {
            if (filter != "All" && !string.Equals(r.Status, filter, StringComparison.OrdinalIgnoreCase)) continue;
            var item = new ListViewItem(new[] { r.Service, r.Status, r.Info ?? string.Empty });
            lvServices.Items.Add(item);
        }
    }

    private async Task ScanHttp(string name, int port, CancellationToken token, bool https = false)
    {
        string status = "Offline"; string? info = null;
        try
        {
            var uri = new Uri($"{(https ? "https" : "http")}://{ip}:{port}/");
            ServicePointManager.ServerCertificateValidationCallback = (m, c, ch, e) => true;
            var req = (HttpWebRequest)WebRequest.Create(uri);
            req.Method = "HEAD";
            req.Timeout = 2000;
            using var resp = (HttpWebResponse)await Task.Run(() => req.GetResponse(), token);
            status = ((int)resp.StatusCode >= 200 && (int)resp.StatusCode < 400) ? "Online" : "Error";
            info = resp.Headers["Server"];
        }
        catch (TaskCanceledException) { status = "Timeout"; }
        catch { status = "Offline"; }
        pendingResults.Enqueue(new ServiceResult(name, status, info));
    }

    private async Task ScanFtp(int port, CancellationToken token)
    {
        string status = "Offline"; string? info = null;
        try
        {
            using var client = new TcpClient();
            var connectTask = client.ConnectAsync(ip, port);
            var delay = Task.Delay(2000, token);
            var completed = await Task.WhenAny(connectTask, delay);
            if (completed == connectTask && client.Connected)
            {
                status = "Online";
                using var stream = client.GetStream();
                stream.ReadTimeout = 2000;
                var buffer = new byte[256];
                int read = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                if (read > 0) info = System.Text.Encoding.ASCII.GetString(buffer, 0, read).Trim();
            }
        }
        catch (TaskCanceledException) { status = "Timeout"; }
        catch { status = "Offline"; }
        pendingResults.Enqueue(new ServiceResult($"FTP {port}", status, info));
    }

    private void Log(string line)
    {
        pendingLogs.Enqueue(line + "\n");
    }

    /// <summary>
    /// Periodicky vypisuje nashromážděné logy do konzole formuláře.
    /// </summary>
    private void UiTimer_Tick(object? sender, EventArgs e)
    {
        if (isClosing || IsDisposed || !IsHandleCreated) return;
        if (rtbLog == null || rtbLog.IsDisposed) return;
        int processed = 0;
        while (processed < 2000 && pendingResults.TryDequeue(out var r))
        {
            processed++;
            lock (all) all.Add(r);
            var filter = cmbStatus.SelectedItem?.ToString() ?? "All";
            if (filter == "All" || string.Equals(r.Status, filter, StringComparison.OrdinalIgnoreCase))
            {
                lvServices.Items.Add(new ListViewItem(new[] { r.Service, r.Status, r.Info ?? string.Empty }));
            }
        }
        if (pendingLogs.Count > 0)
        {
            var sb = new StringBuilder();
            int logs = 0;
            while (logs < 200 && pendingLogs.TryDequeue(out var l))
            {
                sb.Append(l);
                logs++;
            }
            if (sb.Length > 0)
            {
                rtbLog.AppendText(sb.ToString());
                rtbLog.SelectionStart = rtbLog.TextLength;
                rtbLog.ScrollToCaret();
            }
        }
    }

    /// <summary>
    /// Bezpečné ukončení: zastaví timer, zruší úlohy a zabrání zápisu do UI.
    /// </summary>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        isClosing = true;
        try { cts?.Cancel(); } catch { }
        if (uiTimer != null)
        {
            try { uiTimer.Tick -= UiTimer_Tick; } catch { }
            try { uiTimer.Stop(); } catch { }
            try { uiTimer.Dispose(); } catch { }
            uiTimer = null;
        }
        base.OnFormClosing(e);
    }

    private sealed class ServiceResult
    {
        public string Service { get; }
        public string Status { get; }
        public string? Info { get; }
        public ServiceResult(string service, string status, string? info)
        { Service = service; Status = status; Info = info; }
    }
}
