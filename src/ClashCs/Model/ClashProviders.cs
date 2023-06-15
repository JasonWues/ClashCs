using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ClashCs.Model;

[YamlSerializable]
public class ClashProviders
{
    public Dictionary<string, ProvidersItem> providers { get; set; }
    
}

[YamlSerializable]
public class ProvidersItem
{
    public string name { get; set; }
    public ProxiesItem[] proxies { get; set; }
    public string type { get; set; }
    public string vehicleType { get; set; }
}