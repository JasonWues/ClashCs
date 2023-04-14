using YamlDotNet.Serialization;

namespace ClashCs.Server.Entity;

public class Config
{
    [YamlMember(Alias = "port", ApplyNamingConventions = false)]
    public long Port { get; set; }

    [YamlMember(Alias = "socks-port", ApplyNamingConventions = false)]
    public string SocksPort { get; set; }

    [YamlMember(Alias = "mixed-port", ApplyNamingConventions = false)]
    public string MixedPort { get; set; }

    [YamlMember(Alias = "allow-lan", ApplyNamingConventions = false)]
    public bool AllowLan { get; set; }

    [YamlMember(Alias = "bind-address", ApplyNamingConventions = false)]
    public string BindAddress { get; set; }

    [YamlMember(Alias = "mode", ApplyNamingConventions = false)]
    public string Mode { get; set; }

    [YamlMember(Alias = "log-level", ApplyNamingConventions = false)]
    public string LogLevel { get; set; }

    [YamlMember(Alias = "ipv6", ApplyNamingConventions = false)]
    public bool Ipv6 { get; set; }

    [YamlMember(Alias = "external-controller", ApplyNamingConventions = false)]
    public string ExternalController { get; set; }

    [YamlMember(Alias = "secret", ApplyNamingConventions = false)]
    public string Secret { get; set; }

    [YamlMember(Alias = "external-ui", ApplyNamingConventions = false)]
    public string ExternalUi { get; set; }

    [YamlMember(Alias = "interface-name", ApplyNamingConventions = false)]
    public string InterfaceName { get; set; }

    [YamlMember(Alias = "routing-mark", ApplyNamingConventions = false)]
    public long RoutingMark { get; set; }

    [YamlMember(Alias = "hosts", ApplyNamingConventions = false)]
    public object Hosts { get; set; }

    [YamlMember(Alias = "dns", ApplyNamingConventions = false)]
    public DnsInfo Dns { get; set; }

    [YamlMember(Alias = "proxies", ApplyNamingConventions = false)]
    public Proxy[] Proxies { get; set; }

    [YamlMember(Alias = "proxy-groups", ApplyNamingConventions = false)]
    public ProxyGroup[] ProxyGroups { get; set; }

    //[YamlMember(Alias = "proxy-providers", ApplyNamingConventions = false)]
    //public ProxyProviders ProxyProviders { get; set; }

    //[YamlMember(Alias = "tunnels", ApplyNamingConventions = false)]
    //public TunnelElement[] Tunnels { get; set; }

    [YamlMember(Alias = "rules", ApplyNamingConventions = false)]
    public string[] Rules { get; set; }
}

public class DnsInfo
{
    [YamlMember(Alias = "enable", ApplyNamingConventions = false)]
    public bool Enable { get; set; }

    [YamlMember(Alias = "listen", ApplyNamingConventions = false)]
    public string Listen { get; set; }

    [YamlMember(Alias = "default-nameserver", ApplyNamingConventions = false)]
    public string[] DefaultNameserver { get; set; }

    [YamlMember(Alias = "enhanced-mode", ApplyNamingConventions = false)]
    public string EnhancedMode { get; set; }

    [YamlMember(Alias = "fake-ip-range", ApplyNamingConventions = false)]
    public string FakeIpRange { get; set; }

    [YamlMember(Alias = "fallback", ApplyNamingConventions = false)]
    public string[] Fallback { get; set; }

    [YamlMember(Alias = "nameserver", ApplyNamingConventions = false)]
    public string[] Nameserver { get; set; }
}