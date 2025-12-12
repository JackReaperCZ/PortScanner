using NUnit.Framework;
using System.Linq;
using PortScanner.Utils;
using System.Net;

namespace PortScanner.Tests;

public class IpUtilsMoreTests
{
    [Test]
    public void TryParseIPv4_ZeroAndBroadcast()
    {
        var okZero = IpUtils.TryParseIPv4("0.0.0.0", out var v0);
        var okBrd = IpUtils.TryParseIPv4("255.255.255.255", out var v1);
        Assert.That(okZero, Is.True);
        Assert.That(v0, Is.EqualTo(0UL));
        Assert.That(okBrd, Is.True);
        Assert.That(v1, Is.EqualTo(4294967295UL));
    }

    [Test]
    public void TryParseIPv4_RejectsIPv6()
    {
        var ok = IpUtils.TryParseIPv4("fe80::1", out var v);
        Assert.That(ok, Is.False);
        Assert.That(v, Is.EqualTo(0UL));
    }

    [Test]
    public void IpToUInt32_InvalidTextReturnsZero()
    {
        var v = IpUtils.IpToUInt32("1.2.3");
        Assert.That(v, Is.EqualTo(0U));
    }

    [Test]
    public void FromUInt32_EdgeValues()
    {
        Assert.That(IpUtils.FromUInt32(0U).ToString(), Is.EqualTo("0.0.0.0"));
        Assert.That(IpUtils.FromUInt32(uint.MaxValue).ToString(), Is.EqualTo("255.255.255.255"));
    }

    [Test]
    public void ParsePorts_FiltersOnlyValidRange()
    {
        var ports = IpUtils.ParsePorts("-1 0 65536 1 65535").ToList();
        CollectionAssert.AreEqual(new[] { 1, 65535 }, ports);
    }

    [Test]
    public void ParsePorts_DuplicatesPreserved()
    {
        var ports = IpUtils.ParsePorts("80,80;80").ToList();
        Assert.That(ports.Count, Is.EqualTo(3));
        Assert.That(ports.All(p => p == 80), Is.True);
    }
}
