namespace PortScanner.Utils;

using System.Net;

/// <summary>
/// Pomocné funkce pro práci s IPv4 adresami a seznamem portů.
/// </summary>
public static class IpUtils
{
    /// <summary>
    /// Pokusí se převést textovou IPv4 adresu na 32bitové číslo.
    /// </summary>
    /// <param name="text">Textová IPv4 adresa (např. 172.0.0.1).</param>
    /// <param name="value">Výstupní 32bitová hodnota reprezentující IPv4.</param>
    /// <returns>True pokud šlo o validní IPv4, jinak false.</returns>
    public static bool TryParseIPv4(string text, out ulong value)
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

    /// <summary>
    /// Vytvoří <see cref="IPAddress"/> ze 32bitové hodnoty.
    /// </summary>
    /// <param name="v">32bitová hodnota IPv4.</param>
    /// <returns>Instanci <see cref="IPAddress"/>.</returns>
    public static IPAddress FromUInt32(uint v)
    {
        var b0 = (byte)((v >> 24) & 0xFF);
        var b1 = (byte)((v >> 16) & 0xFF);
        var b2 = (byte)((v >> 8) & 0xFF);
        var b3 = (byte)(v & 0xFF);
        return new IPAddress(new byte[] { b0, b1, b2, b3 });
    }

    /// <summary>
    /// Převede textovou IPv4 na 32bitovou hodnotu; nevalidní vrací 0.
    /// </summary>
    /// <param name="s">Textová IPv4.</param>
    /// <returns>32bitová reprezentace nebo 0 pro nevalidní.</returns>
    public static uint IpToUInt32(string s)
    {
        if (IPAddress.TryParse(s, out var ip))
        {
            var b = ip.GetAddressBytes();
            return (uint)b[0] << 24 | (uint)b[1] << 16 | (uint)b[2] << 8 | (uint)b[3];
        }
        return 0;
    }

    /// <summary>
    /// Rozparsuje text se seznamem portů do kolekce validních čísel portů.
    /// </summary>
    /// <param name="text">Seznam portů oddělený čárkou/ středníkem/ mezerou.</param>
    /// <returns>Enumerable validních portů v rozsahu 1–65535.</returns>
    public static IEnumerable<int> ParsePorts(string text)
    {
        var list = new List<int>();
        foreach (var part in text.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (int.TryParse(part, out var p) && p > 0 && p <= 65535)
                list.Add(p);
        }
        return list;
    }
}
