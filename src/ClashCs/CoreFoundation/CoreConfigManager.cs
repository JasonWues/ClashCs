using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClashCs.Config;
using ClashCs.Model;
using ClashCs.Tool;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClashCs.CoreFoundation;

public class CoreConfigManager
{
    public async Task GenerateClientConfigAsync(ProfileItem profileItem)
    {
        if (profileItem == null)
        {
            return;
        }

        try
        {
            var address = profileItem.Address;
            if (string.IsNullOrEmpty(address))
            {
                return;
            }
            if (!File.Exists(address))
            {
                return;
            }


            var yaml = await File.ReadAllTextAsync(address);
            
            var z  = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            var zx = z.Deserialize(new StringReader(yaml));

            /*var fileLines = File.ReadLinesAsync(address);
            var localConfig = LazyConfig.Instance.Value.LocalConfig;
            StringBuilder stringBuilder = new StringBuilder();
            await foreach (var content in fileLines)
            {
                var content2 = content switch
                {
                    { } s when s.StartsWith("port:") => $"port: {localConfig.HttpPort}",
                    { } s when s.StartsWith("mixed-port:") => $"mixed-port: {localConfig.MixedPort}",
                    { } s when s.StartsWith("socks-port:") => $"socks-port {localConfig.SocksPort}",
                    { } s when s.StartsWith("log-level:") => $"log-level: {localConfig.LogLevel}",
                    { } s when s.StartsWith("external-controller:") => $"external-controller: {Global.Loopback}:{localConfig.ApiPort}",
                    { } s when s.StartsWith("ipv6:") => $"ipv6: {localConfig.EnableIpv6}",


                    _ => content
                };
                stringBuilder.AppendLine(content2);
            }

            var file = stringBuilder.ToString();*/

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}