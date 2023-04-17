using ClashCs.Config;
using MemoryPack;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClashCs;

public static class Util  
{
    public static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    public static string UA =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.34";

    public static string LocalConfigPath = Path.Combine(Environment.CurrentDirectory,".config","config");

    public static async ValueTask<LocalConfig?> ReadConfigAsync()
    {
        await using var readStrean = File.OpenRead(LocalConfigPath);
        return await MemoryPackSerializer.DeserializeAsync<LocalConfig>(readStrean);
    }

    public static async ValueTask WriteConfigAsync(LocalConfig localConfig)
    {
        await using var writeStream = File.OpenWrite(LocalConfigPath);
        await MemoryPackSerializer.SerializeAsync(writeStream,localConfig);
    }
}