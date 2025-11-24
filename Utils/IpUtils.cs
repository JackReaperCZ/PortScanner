namespace PortScanner.Utils;

using System.Net;

public static class IpUtils
{
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

    public static IPAddress FromUInt32(uint v)
    {
        var b0 = (byte)((v >> 24) & 0xFF);
        var b1 = (byte)((v >> 16) & 0xFF);
        var b2 = (byte)((v >> 8) & 0xFF);
        var b3 = (byte)(v & 0xFF);
        return new IPAddress(new byte[] { b0, b1, b2, b3 });
    }

    public static uint IpToUInt32(string s)
    {
        if (IPAddress.TryParse(s, out var ip))
        {
            var b = ip.GetAddressBytes();
            return (uint)b[0] << 24 | (uint)b[1] << 16 | (uint)b[2] << 8 | (uint)b[3];
        }
        return 0;
    }

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
