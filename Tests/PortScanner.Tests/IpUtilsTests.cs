using NUnit.Framework;
using System.Linq;
using PortScanner.Utils;

namespace PortScanner.Tests;

public class IpUtilsTests
{
    [Test]
    public void TryParseIPv4_Valid()
    {
        var ok = IpUtils.TryParseIPv4("172.0.0.1", out var value);
        Assert.That(ok, Is.True);
        Assert.That(value, Is.GreaterThan(0));
    }

    [Test]
    public void TryParseIPv4_Invalid()
    {
        var ok = IpUtils.TryParseIPv4("999.999.999.999", out var value);
        Assert.That(ok, Is.False);
        Assert.That(value, Is.EqualTo(0));
    }

    [Test]
    public void IpToUInt32_RoundTrip()
    {
        var v = IpUtils.IpToUInt32("1.2.3.4");
        var ip = IpUtils.FromUInt32(v);
        Assert.That(ip.ToString(), Is.EqualTo("1.2.3.4"));
    }

    [Test]
    public void ParsePorts_MixedInput()
    {
        var ports = IpUtils.ParsePorts("80,443; 22 abc 0 70000").ToList();
        CollectionAssert.AreEquivalent(new[] { 80, 443, 22 }, ports);
    }
}
