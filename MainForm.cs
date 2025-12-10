namespace PortScanner;

using System.Collections.Concurrent;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Linq;
using PortScanner.Models;
using PortScanner.Services;
using PortScanner.Utils;
using System.Windows.Forms;

/// <summary>
/// Hlavní formulář aplikace: obsahuje GUI, filtry, exporty a bezpečné batchování výsledků.
/// </summary>
public partial class MainForm : Form
{
    private readonly object listViewLock = new object();
    private readonly List<ScanRecord> allResults = new List<ScanRecord>();
    private List<ScanRecord> filteredResults = new List<ScanRecord>();
    private readonly System.Collections.Concurrent.ConcurrentQueue<ScanRecord> pendingResults = new System.Collections.Concurrent.ConcurrentQueue<ScanRecord>();
    private readonly System.Collections.Concurrent.ConcurrentQueue<string> pendingLogs = new System.Collections.Concurrent.ConcurrentQueue<string>();
    private System.Windows.Forms.Timer? uiTimer;
    private Scanner? scanner;
    private Action<ScanRecord>? recordHandler;
    private Action<string>? logHandler;
    private Action? completedHandler;
    private string currentIpFilter = string.Empty;
    private string currentHostFilter = string.Empty;
    private string currentStatusFilter = "All";
    private int? currentRttMin = null;
    private int? currentRttMax = null;
    private int currentSortColumn = 0;
    private SortOrder currentSortOrder = SortOrder.None;

    /// <summary>
    /// Inicializuje komponenty a časovač pro dávkové UI aktualizace.
    /// </summary>
    public MainForm()
    {
        InitializeComponent();
        MinimumSize = Size;
        uiTimer = new System.Windows.Forms.Timer();
        uiTimer.Interval = 100;
        uiTimer.Tick += UiTimer_Tick;
        tsmiStop.Enabled = false;
    }

    /// <summary>
    /// Spustí sken dle aktuální konfigurace GUI.
    /// </summary>
    private void btnStart_Click(object? sender, EventArgs e)
    {
        if (scanner != null)
            return;
        string startText;
        string endText;
        if (tabInput.SelectedTab == tabSubnet)
        {
            var net = IpUtils.IpToUInt32(txtNetIp.Text.Trim());
            var mask = IpUtils.IpToUInt32(txtMask.Text.Trim());
            if (net == 0 || mask == 0)
            {
                MessageBox.Show(this, "Neplatná síť nebo maska", "Vstup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var network = net & mask;
            var broadcast = network | ~mask;
            startText = IpUtils.FromUInt32(network).ToString();
            endText = IpUtils.FromUInt32(broadcast).ToString();
        }
        else
        {
            startText = txtStartIpRange.Text.Trim();
            endText = txtEndIpRange.Text.Trim();
            if (!IpUtils.TryParseIPv4(startText, out var startVal) || !IpUtils.TryParseIPv4(endText, out var endVal))
            {
                MessageBox.Show(this, "Neplatný formát IP adresy (rozsah)", "Vstup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (startVal > endVal)
            {
                MessageBox.Show(this, "Počáteční IP je větší než koncová", "Vstup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        if (numWorkers.Value <= 0)
        {
            MessageBox.Show(this, "Počet workerů musí být alespoň 1", "Vstup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        ClearResults();
        DrainQueues();
        scanner = new Scanner(startText, endText, (int)numWorkers.Value, chkDns.Checked);
        recordHandler = r => pendingResults.Enqueue(r);
        logHandler = line => pendingLogs.Enqueue(line);
        completedHandler = () => CleanupRun();
        scanner.Record += recordHandler;
        scanner.Log += logHandler;
        scanner.Completed += completedHandler;
        scanner.Start();
        uiTimer!.Start();
        tsmiStart.Enabled = false;
        tsmiStop.Enabled = true;
    }

    /// <summary>
    /// Zastaví aktuální běžící sken.
    /// </summary>
    private void btnStop_Click(object? sender, EventArgs e)
    {
        if (scanner == null) return;
        try { scanner.Record -= recordHandler; } catch { }
        try { scanner.Log -= logHandler; } catch { }
        try { scanner.Completed -= completedHandler; } catch { }
        scanner.Stop();
        scanner = null;
        uiTimer!.Stop();
        tsmiStart.Enabled = true;
        tsmiStop.Enabled = false;
    }

    /// <summary>
    /// Vymaže tabulku a interní dataset výsledků.
    /// </summary>
    private void btnClearOutput_Click(object? sender, EventArgs e)
    {
        ClearResults();
    }

    /// <summary>
    /// Vymaže logovací konzoli.
    /// </summary>
    private void btnClearLog_Click(object? sender, EventArgs e)
    {
        rtbLog.Clear();
    }

    /// <summary>
    /// Exportuje aktuálně filtrované výsledky do CSV.
    /// </summary>
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
                sb.AppendLine(string.Join(",", new[] { EscapeCsv(ip), EscapeCsv(status), EscapeCsv(rtt), EscapeCsv(host) }));
            }
            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
        }
    }

    /// <summary>
    /// Exportuje aktuálně filtrované výsledky do JSON.
    /// </summary>
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
            var json = JsonConvert.SerializeObject(results, Formatting.Indented);
            File.WriteAllText(sfd.FileName, json, Encoding.UTF8);
        }
    }

    /// <summary>
    /// Exportuje aktuálně filtrované výsledky do XML.
    /// </summary>
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
        if (s.Contains("\"") || s.Contains(",") || s.Contains("\n"))
            return "\"" + s.Replace("\"", "\"\"") + "\"";
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

    /// <summary>
    /// Řazení – nastaví sloupec a směr řazení a obnoví virtuální list.
    /// </summary>
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

    /// <summary>
    /// Aplikuje filtry z ovládacích prvků.
    /// </summary>
    private void btnApplyFilter_Click(object? sender, EventArgs e)
    {
        ApplyFilterFromControls();
    }

    /// <summary>
    /// Resetuje filtry na výchozí hodnoty a přestaví zobrazení.
    /// </summary>
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

    /// <summary>
    /// Načte hodnoty filtrů z GUI do interních proměnných.
    /// </summary>
    private void ApplyFilterFromControls()
    {
        currentIpFilter = txtFilterIp.Text.Trim();
        currentHostFilter = txtFilterHost.Text.Trim();
        currentStatusFilter = cmbStatus.SelectedItem?.ToString() ?? "All";
        currentRttMin = int.TryParse(txtRttMin.Text.Trim(), out var mn) ? mn : (int?)null;
        currentRttMax = int.TryParse(txtRttMax.Text.Trim(), out var mx) ? mx : (int?)null;
        RebuildView();
    }

    /// <summary>
    /// Přestaví virtuální zobrazení dle aktuálních filtrů.
    /// </summary>
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

    /// <summary>
    /// Ověří, zda daný záznam splňuje aktuálně nastavené filtry.
    /// </summary>
    private bool MatchesFilters(ScanRecord r)
    {
        if (!string.IsNullOrEmpty(currentIpFilter) && r.Ip.IndexOf(currentIpFilter, StringComparison.OrdinalIgnoreCase) < 0) return false;
        if (!string.IsNullOrEmpty(currentHostFilter) && (r.Hostname == null || r.Hostname.IndexOf(currentHostFilter, StringComparison.OrdinalIgnoreCase) < 0)) return false;
        if (currentStatusFilter != "All" && !string.Equals(r.Status, currentStatusFilter, StringComparison.OrdinalIgnoreCase)) return false;
        if (currentRttMin.HasValue && (!r.Rtt.HasValue || r.Rtt.Value < currentRttMin.Value)) return false;
        if (currentRttMax.HasValue && (!r.Rtt.HasValue || r.Rtt.Value > currentRttMax.Value)) return false;
        return true;
    }

    /// <summary>
    /// Úklid po dokončení běhu skenu.
    /// </summary>
    private void CleanupRun()
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(CleanupRun));
            return;
        }
        uiTimer!.Stop();
        scanner = null;
        tsmiStart.Enabled = true;
        tsmiStop.Enabled = false;
    }

    /// <summary>
    /// Dodá položku pro virtuální ListView dle indexu.
    /// </summary>
    private void lvResults_RetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
    {
        ScanRecord rec;
        lock (allResults)
        {
            if (e.ItemIndex < 0 || e.ItemIndex >= filteredResults.Count)
            {
                e.Item = new ListViewItem(new[] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
                return;
            }
            rec = filteredResults[e.ItemIndex];
        }
        e.Item = new ListViewItem(new[]
        {
            rec.Ip,
            rec.Status,
            rec.Rtt.HasValue ? rec.Rtt.Value.ToString() : string.Empty,
            rec.Hostname ?? string.Empty,
            rec.Mac ?? string.Empty
        });
    }

    private void lvResults_DoubleClick(object? sender, EventArgs e)
    {
        if (lvResults.SelectedIndices.Count == 0) return;
        var idx = lvResults.SelectedIndices[0];
        ScanRecord rec;
        lock (allResults)
        {
            if (idx < 0 || idx >= filteredResults.Count) return;
            rec = filteredResults[idx];
        }
        using var details = new HostDetailsForm(rec.Ip, rec.Hostname, rec.Mac);
        details.ShowDialog(this);
    }

    
    
    /// <summary>
    /// Periodicky zpracuje dávku přijatých výsledků a logů na UI threadu.
    /// </summary>
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

    private void ClearResults()
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

    private void DrainQueues()
    {
        while (pendingResults.TryDequeue(out var _)) { }
        while (pendingLogs.TryDequeue(out var _)) { }
    }

    

    /// <summary>
    /// Setřídí aktuální filtrované výsledky dle zvoleného sloupce a směru.
    /// </summary>
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
        else if (currentSortColumn == 3)
        {
            cmp = (a, b) => string.Compare(a.Hostname ?? string.Empty, b.Hostname ?? string.Empty, StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            cmp = (a, b) => string.Compare(a.Mac ?? string.Empty, b.Mac ?? string.Empty, StringComparison.OrdinalIgnoreCase);
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
