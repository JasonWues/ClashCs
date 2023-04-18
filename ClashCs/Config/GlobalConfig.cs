namespace ClashCs.Config;

public static class GlobalConfig
{
    public static LocalConfig LocalConfig { get; set; } = new LocalConfig();

    public static ProxyConfig ProxyConfig { get; set; } = new ProxyConfig();
}