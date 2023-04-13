using YamlDotNet.Serialization;

namespace ClashCs.Server.Model
{
    public class Proxy
    {
        [YamlMember(Alias = "name",ApplyNamingConventions = false)]
        public string Name { get; set; }

        [YamlMember(Alias = "type",ApplyNamingConventions = false)]
        public string Type { get; set; }

        [YamlMember(Alias = "server",ApplyNamingConventions = false)]
        public string Server { get; set; }

        [YamlMember(Alias = "port",ApplyNamingConventions = false)]
        public long Port { get; set; }

        [YamlMember(Alias = "cipher",ApplyNamingConventions = false)]
        public string Cipher { get; set; }

        [YamlMember(Alias = "password",ApplyNamingConventions = false)]
        public string Password { get; set; }

        [YamlMember(Alias = "plugin",ApplyNamingConventions = false)]
        public string Plugin { get; set; }

        [YamlMember(Alias = "plugin-opts",ApplyNamingConventions = false)]
        public PluginOpts PluginOpts { get; set; }

        [YamlMember(Alias = "uuid",ApplyNamingConventions = false)]
        public string Uuid { get; set; }

        [YamlMember(Alias = "alterId",ApplyNamingConventions = false)]
        public long? AlterId { get; set; }

        [YamlMember(Alias = "ws-opts",ApplyNamingConventions = false)]
        public WsOpts WsOpts { get; set; }

        [YamlMember(Alias = "network",ApplyNamingConventions = false)]
        public string Network { get; set; }

        [YamlMember(Alias = "tls",ApplyNamingConventions = false)]
        public bool? Tls { get; set; }

        [YamlMember(Alias = "h2-opts",ApplyNamingConventions = false)]
        public H2Opts H2Opts { get; set; }

        [YamlMember(Alias = "http-opts" ,ApplyNamingConventions = false)]
        public HttpOpts HttpOpts { get; set; }

        [YamlMember(Alias = "servername",ApplyNamingConventions = false)]
        public string Servername { get; set; }

        [YamlMember(Alias = "grpc-opts",ApplyNamingConventions = false)]
        public GrpcOpts GrpcOpts { get; set; }

        [YamlMember(Alias = "psk",ApplyNamingConventions = false)]
        public string Psk { get; set; }

        [YamlMember(Alias = "sni",ApplyNamingConventions = false)]
        public string Sni { get; set; }

        [YamlMember(Alias = "udp",ApplyNamingConventions = false)]
        public bool? Udp { get; set; }

        [YamlMember(Alias = "obfs",ApplyNamingConventions = false)]
        public string Obfs { get; set; }

        [YamlMember(Alias = "protocol",ApplyNamingConventions = false)]
        public string Protocol { get; set; }
    }

    public class H2Opts
    {
        [YamlMember(Alias = "host",ApplyNamingConventions = false)]
        public string[] Host { get; set; }

        [YamlMember(Alias = "path",ApplyNamingConventions = false)]
        public string Path { get; set; }
    }

    public class PluginOpts
    {
        [YamlMember(Alias = "mode",ApplyNamingConventions = false)]
        public string Mode { get; set; }

        [YamlMember(Alias = "tls",ApplyNamingConventions = false)]
        public bool? Tls { get; set; }

        [YamlMember(Alias = "skip-cert-verify",ApplyNamingConventions = false)]
        public bool? SkipCertVerify { get; set; }

        [YamlMember(Alias = "host",ApplyNamingConventions = false)]
        public string Host { get; set; }

        [YamlMember(Alias = "path",ApplyNamingConventions = false)]
        public string Path { get; set; }

        [YamlMember(Alias = "mux",ApplyNamingConventions = false)]
        public bool? Mux { get; set; }

        [YamlMember(Alias = "headers",ApplyNamingConventions = false)]
        public PluginOptsHeaders Headers { get; set; }
    }

    public partial class WsOpts
    {
        [YamlMember(Alias = "path",ApplyNamingConventions = false)]
        public string Path { get; set; }

        [YamlMember(Alias = "headers",ApplyNamingConventions = false)]
        public WsOptsHeaders Headers { get; set; }

        [YamlMember(Alias = "max-early-data",ApplyNamingConventions = false)]
        public long MaxEarlyData { get; set; }

        [YamlMember(Alias = "early-data-header-name",ApplyNamingConventions = false)]
        public string EarlyDataHeaderName { get; set; }
    }

    public partial class HttpOpts
    {
        [YamlMember(Alias = "method",ApplyNamingConventions = false)]
        public string Method { get; set; }

        [YamlMember(Alias = "path",ApplyNamingConventions = false)]
        public string[] Path { get; set; }

        [YamlMember(Alias = "headers",ApplyNamingConventions = false)]
        public HttpOptsHeaders Headers { get; set; }
    }
    public class GrpcOpts
    {
        [YamlMember(Alias = "grpc-service-name",ApplyNamingConventions = false)]
        public string GrpcServiceName { get; set; }
    }

    public class HttpOptsHeaders
    {
        [YamlMember(Alias = "Connection",ApplyNamingConventions = false)]
        public string[] Connection { get; set; }
    }

    public class PluginOptsHeaders
    {
        [YamlMember(Alias = "custom",ApplyNamingConventions = false)]
        public string Custom { get; set; }
    }

    public class WsOptsHeaders
    {
        [YamlMember(Alias = "Host",ApplyNamingConventions = false)]
        public string Host { get; set; }

        [YamlMember(Alias = "Edge", ApplyNamingConventions = false)]
        public string Edge { get; set; }
    }

    public enum Cipher { Chacha20IetfPoly1305, Chacha20Poly1305 };

}
