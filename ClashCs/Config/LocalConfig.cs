using MemoryPack;
using YamlDotNet.Serialization;

namespace ClashCs.Config;

[MemoryPackable]
public partial class LocalConfig
{
    public bool IsBoot { get; set; } = false;

    public List<LocalProxyConfig> LocalProxyConfigs { get; set; }
}

public class LocalProxyConfig
{
    public string FileName { get; set; }

    public string SubscriptionName { get; set; }

    public string Url { get; set; }

    public string? HomeLink { get; set; }


    public Dictionary<string, string> Selected { get; set; }

    public SubInfo? SubInfo { get; set; }

    public int UpdateInterval { get; private set; }

    public void SetUpdateInterval(string s)
    {
        UpdateInterval = !string.IsNullOrWhiteSpace(s) ? int.Parse(s) : 24;
    }
}

public class SubInfo
{
    [YamlMember(Alias = "upload", ApplyNamingConventions = false)]
    public int Upload { get; set; }

    [YamlMember(Alias = "download", ApplyNamingConventions = false)]
    public string Download { get; set; }

    [YamlMember(Alias = "total", ApplyNamingConventions = false)]
    public string Total { get; set; }

    [YamlMember(Alias = "expire", ApplyNamingConventions = false)]
    public string Expire { get; set; }
}