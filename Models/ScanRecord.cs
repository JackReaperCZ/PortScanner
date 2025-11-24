namespace PortScanner.Models;

public sealed class ScanRecord
{
    public string Ip { get; }
    public string Status { get; }
    public int? Rtt { get; }
    public string? Hostname { get; }
    public ScanRecord(string ip, string status, int? rtt, string? hostname)
    { Ip = ip; Status = status; Rtt = rtt; Hostname = hostname; }
}
