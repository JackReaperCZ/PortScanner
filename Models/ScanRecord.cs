namespace PortScanner.Models;

/// <summary>
/// Reprezentuje jeden výsledek skenu (IP, stav, latence, hostname).
/// </summary>
public sealed class ScanRecord
{
    /// <summary>
    /// IPv4 adresa cíle ve string formátu.
    /// </summary>
    public string Ip { get; }

    /// <summary>
    /// Stav výsledku (např. Online, Offline, Timeout, Error).
    /// </summary>
    public string Status { get; }

    /// <summary>
    /// Latence ping v milisekundách, pokud je dostupná.
    /// </summary>
    public int? Rtt { get; }

    /// <summary>
    /// Vyhodnocené jméno hostitele nebo doplňkové informace (např. otevřené porty).
    /// </summary>
    public string? Hostname { get; }

    public string? Mac { get; }

    /// <summary>
    /// Vytvoří novou instanci výsledku skenu.
    /// </summary>
    /// <param name="ip">IPv4 adresa cíle.</param>
    /// <param name="status">Textový stav výsledku.</param>
    /// <param name="rtt">Latence v ms (volitelné).</param>
    /// <param name="hostname">Hostname nebo doplňkové info (volitelné).</param>
    public ScanRecord(string ip, string status, int? rtt, string? hostname, string? mac = null)
    { Ip = ip; Status = status; Rtt = rtt; Hostname = hostname; Mac = mac; }
}
