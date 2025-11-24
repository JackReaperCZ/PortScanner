namespace PortScanner;

using System.Collections.Concurrent;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using PortScanner.Models;
using PortScanner.Services;
using PortScanner.Utils;

public partial class Form1 : Form
{
    private readonly object listViewLock = new object();
    private readonly List<ScanRecord> allResults = new List<ScanRecord>();
    private List<ScanRecord> filteredResults = new List<ScanRecord>();
    private readonly System.Collections.Concurrent.ConcurrentQueue<ScanRecord> pendingResults = new System.Collections.Concurrent.ConcurrentQueue<ScanRecord>();
    private readonly System.Collections.Concurrent.ConcurrentQueue<string> pendingLogs = new System.Collections.Concurrent.ConcurrentQueue<string>();
    private System.Windows.Forms.Timer? uiTimer;
    private Scanner? scanner;
    private string currentIpFilter = string.Empty;
    private string currentHostFilter = string.Empty;
    private string currentStatusFilter = "All";
    private int? currentRttMin = null;
    private int? currentRttMax = null;
    private int currentSortColumn = 0;
    private SortOrder currentSortOrder = SortOrder.None;

    public Form1()
    {
        InitializeComponent();
        uiTimer = new System.Windows.Forms.Timer();
        uiTimer.Interval = 100;
        uiTimer.Tick += UiTimer_Tick;
    }

    private void btnStart_Click(object? sender, EventArgs e)
    {
        if (scanner != null)
            return;
        scanner = new Scanner(txtStartIp.Text.Trim(), txtEndIp.Text.Trim(), (int)numWorkers.Value, chkDns.Checked, chkPortScan.Checked, IpUtils.ParsePorts(txtPorts.Text));
        scanner.Record += r => pendingResults.Enqueue(r);
        scanner.Log += line => pendingLogs.Enqueue(line);
        scanner.Completed += () => CleanupRun();
        scanner.Start();
        uiTimer!.Start();
    }

    private void btnStop_Click(object? sender, EventArgs e)
    {
        scanner?.Stop();
    }

    private void btnClearOutput_Click(object? sender, EventArgs e)
    {
        lock (listViewLock)
        {
            lvResults.VirtualListSize = 0;
            lvResults.Invalidate();
        }
        lock (allResults)
        {
            allResults.Clear();
            filteredResults.Clear();
        }
    }

    private void btnClearLog_Click(object? sender, EventArgs e)
    {
        rtbLog.Clear();
    }

    private void btnExportCsv_Click(object? sender, EventArgs e)
    {
        if (filteredResults.Count == 0)
        {
            MessageBox.Show(this, "Tabulka je prázdná", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        using var sfd = new SaveFileDialog { Filter = "CSV Files|*.csv", FileName = "scan.csv" };
        if (sfd.ShowDialog(this) == DialogResult.OK)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Ip,Status,RTT,Hostname");
            for (int i = 0; i < filteredResults.Count; i++)
            {
                var rec = filteredResults[i];
                var ip = rec.Ip;
                var status = rec.Status;
                var rtt = rec.Rtt.HasValue ? rec.Rtt.Value.ToString() : string.Empty;
                var host = rec.Hostname ?? string.Empty;
                sb.AppendLine(string.Join(',', new[] { EscapeCsv(ip), EscapeCsv(status), EscapeCsv(rtt), EscapeCsv(host) }));
            }
            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
        }
    }

    private void btnExportJson_Click(object? sender, EventArgs e)
    {
        if (filteredResults.Count == 0)
        {
            MessageBox.Show(this, "Tabulka je prázdná", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        using var sfd = new SaveFileDialog { Filter = "JSON Files|*.json", FileName = "scan.json" };
        if (sfd.ShowDialog(this) == DialogResult.OK)
        {
            var results = new List<object>();
            for (int i = 0; i < filteredResults.Count; i++)
            {
                var rec = filteredResults[i];
                results.Add(new
                {
                    ip = rec.Ip,
                    status = rec.Status,
                    latency = rec.Rtt,
                    hostname = rec.Hostname
                });
            }
            var json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(sfd.FileName, json, Encoding.UTF8);
        }
    }

    private void btnExportXml_Click(object? sender, EventArgs e)
    {
        if (filteredResults.Count == 0)
        {
            MessageBox.Show(this, "Tabulka je prázdná", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        using var sfd = new SaveFileDialog { Filter = "XML Files|*.xml", FileName = "scan.xml" };
        if (sfd.ShowDialog(this) == DialogResult.OK)
        {
            var doc = new XDocument(new XElement("ScanResults"));
            for (int i = 0; i < filteredResults.Count; i++)
            {
                var rec = filteredResults[i];
                var el = new XElement("Result",
                    new XElement("IP", rec.Ip),
                    new XElement("Status", rec.Status));
                if (rec.Rtt.HasValue)
                    el.Add(new XElement("RTT", rec.Rtt.Value));
                if (!string.IsNullOrWhiteSpace(rec.Hostname))
                    el.Add(new XElement("Hostname", rec.Hostname));
                doc.Root!.Add(el);
            }
            doc.Save(sfd.FileName);
        }
    }

    

    private void QueueResult(string ip, string status, int? rtt, string? hostname)
    {
        var rec = new ScanRecord(ip, status, rtt, hostname);
        pendingResults.Enqueue(rec);
    }

    private void QueueLog(string line)
    {
        pendingLogs.Enqueue(line);
    }

    private static bool TryParseIPv4(string text, out ulong value)
    {
        value = 0;
        if (IPAddress.TryParse(text, out var ip))
        {
            var bytes = ip.GetAddressBytes();
            if (bytes.Length == 4)
            {
                value = (ulong)bytes[0] << 24 | (ulong)bytes[1] << 16 | (ulong)bytes[2] << 8 | (ulong)bytes[3];
                return true;
            }
        }
        return false;
    }

    

    private static string EscapeCsv(string s)
    {
        if (s.Contains('"') || s.Contains(',') || s.Contains('\n'))
            return '"' + s.Replace("\"", "\"\"") + '"';
        return s;
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

    private void lvResults_ColumnClick(object? sender, ColumnClickEventArgs e)
    {
        if (currentSortColumn == e.Column)
        {
            currentSortOrder = currentSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
        }
        else
        {
            currentSortColumn = e.Column;
            currentSortOrder = SortOrder.Ascending;
        }
        SortFilteredResults();
        lock (listViewLock)
        {
            lvResults.VirtualListSize = filteredResults.Count;
            lvResults.Invalidate();
        }
    }

    private void btnApplyFilter_Click(object? sender, EventArgs e)
    {
        ApplyFilterFromControls();
    }

    private void btnClearFilter_Click(object? sender, EventArgs e)
    {
        currentIpFilter = string.Empty;
        currentHostFilter = string.Empty;
        currentStatusFilter = "All";
        currentRttMin = null;
        currentRttMax = null;
        txtFilterIp.Text = string.Empty;
        txtFilterHost.Text = string.Empty;
        txtRttMin.Text = string.Empty;
        txtRttMax.Text = string.Empty;
        cmbStatus.SelectedIndex = 0;
        RebuildView();
    }

    private void ApplyFilterFromControls()
    {
        currentIpFilter = txtFilterIp.Text.Trim();
        currentHostFilter = txtFilterHost.Text.Trim();
        currentStatusFilter = cmbStatus.SelectedItem?.ToString() ?? "All";
        currentRttMin = int.TryParse(txtRttMin.Text.Trim(), out var mn) ? mn : (int?)null;
        currentRttMax = int.TryParse(txtRttMax.Text.Trim(), out var mx) ? mx : (int?)null;
        RebuildView();
    }

    private void RebuildView()
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(RebuildView));
            return;
        }
        lock (allResults)
        {
            filteredResults = allResults.Where(MatchesFilters).ToList();
        }
        lock (listViewLock)
        {
            lvResults.VirtualListSize = filteredResults.Count;
            lvResults.Invalidate();
        }
        SortFilteredResults();
    }

    private bool MatchesFilters(ScanRecord r)
    {
        if (!string.IsNullOrEmpty(currentIpFilter) && !r.Ip.Contains(currentIpFilter, StringComparison.OrdinalIgnoreCase)) return false;
        if (!string.IsNullOrEmpty(currentHostFilter) && (r.Hostname == null || !r.Hostname.Contains(currentHostFilter, StringComparison.OrdinalIgnoreCase))) return false;
        if (currentStatusFilter != "All" && !string.Equals(r.Status, currentStatusFilter, StringComparison.OrdinalIgnoreCase)) return false;
        if (currentRttMin.HasValue && (!r.Rtt.HasValue || r.Rtt.Value < currentRttMin.Value)) return false;
        if (currentRttMax.HasValue && (!r.Rtt.HasValue || r.Rtt.Value > currentRttMax.Value)) return false;
        return true;
    }

    private void CleanupRun()
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(CleanupRun));
            return;
        }
        uiTimer!.Stop();
        scanner = null;
    }

    private void lvResults_RetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
    {
        ScanRecord rec;
        lock (allResults)
        {
            if (e.ItemIndex < 0 || e.ItemIndex >= filteredResults.Count)
            {
                e.Item = new ListViewItem(new[] { string.Empty, string.Empty, string.Empty, string.Empty });
                return;
            }
            rec = filteredResults[e.ItemIndex];
        }
        e.Item = new ListViewItem(new[]
        {
            rec.Ip,
            rec.Status,
            rec.Rtt.HasValue ? rec.Rtt.Value.ToString() : string.Empty,
            rec.Hostname ?? string.Empty
        });
    }

    private void UiTimer_Tick(object? sender, EventArgs e)
    {
        if (IsDisposed || !IsHandleCreated) return;
        var added = false;
        int processed = 0;
        while (processed < 2000 && pendingResults.TryDequeue(out var rec))
        {
            processed++;
            lock (allResults)
            {
                allResults.Add(rec);
                if (MatchesFilters(rec))
                {
                    filteredResults.Add(rec);
                    added = true;
                }
            }
        }
        if (added)
        {
            SortFilteredResults();
            lock (listViewLock)
            {
                lvResults.VirtualListSize = filteredResults.Count;
                lvResults.Invalidate();
            }
        }
        if (pendingLogs.Count > 0)
        {
            var sb = new StringBuilder();
            int logs = 0;
            while (logs < 200 && pendingLogs.TryDequeue(out var line))
            {
                sb.Append(line);
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

    private void SortFilteredResults()
    {
        Comparison<ScanRecord> cmp;
        if (currentSortColumn == 0)
        {
            cmp = (a, b) => IpUtils.IpToUInt32(a.Ip).CompareTo(IpUtils.IpToUInt32(b.Ip));
        }
        else if (currentSortColumn == 2)
        {
            cmp = (a, b) => (a.Rtt ?? int.MaxValue).CompareTo(b.Rtt ?? int.MaxValue);
        }
        else if (currentSortColumn == 1)
        {
            cmp = (a, b) => string.Compare(a.Status, b.Status, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            cmp = (a, b) => string.Compare(a.Hostname ?? string.Empty, b.Hostname ?? string.Empty, StringComparison.OrdinalIgnoreCase);
        }
        lock (allResults)
        {
            if (currentSortOrder == SortOrder.Descending)
                filteredResults.Sort((a, b) => -cmp(a, b));
            else
                filteredResults.Sort(cmp);
        }
    }

    
}
