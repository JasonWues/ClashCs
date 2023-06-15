using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ClashCs.Model;

[YamlSerializable]
public class ClashProxies
{
    public Dictionary<string, ProxiesItem> proxies { get; set; }
}

[YamlSerializable]
public class ProxiesItem
{
    public string[] all { get; set; }
    public List<HistoryItem> history { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public bool udp { get; set; }
    public string now { get; set; }
    public int delay { get; set; }
}

[YamlSerializable]
public class HistoryItem
{
    public string time { get; set; }
    public int delay { get; set; }
}