using YamlDotNet.Serialization;

namespace ClashCs.Server.Model
{
    public class ProxyGroup
    {
        [YamlMember(Alias = "name", ApplyNamingConventions = false)]
        public string Name { get; set; }

        [YamlMember(Alias = "type", ApplyNamingConventions = false)]
        public string Type { get; set; }

        [YamlMember(Alias = "proxies", ApplyNamingConventions = false)]
        public string[] Proxies { get; set; }

        [YamlMember(Alias = "url", ApplyNamingConventions = false)]
        public Uri Url { get; set; }

        [YamlMember(Alias = "interval", ApplyNamingConventions = false)]
        public long? Interval { get; set; }

        [YamlMember(Alias = "interface-name", ApplyNamingConventions = false)]
        public string InterfaceName { get; set; }

        [YamlMember(Alias = "routing-mark", ApplyNamingConventions = false)]
        public long? RoutingMark { get; set; }

        [YamlMember(Alias = "use", ApplyNamingConventions = false)]
        public string[] Use { get; set; }
    }
}
