using ClashCs.Config;
using MemoryPack;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClashCs;

public static class Util
{
    private static readonly IDeserializer deserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    public static T Deserializer<T>(string input) where T : new()
    {
        if (!string.IsNullOrEmpty(input))
        {
            return deserializer.Deserialize<T>(input);
        }

        return new T();
    }

    public static T Deserializer<T>(TextReader input)
    {
        return deserializer.Deserialize<T>(input);
    }

    public static string UA =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36 Edg/112.0.1722.34";

    public static string LocalConfigPath = Path.Combine(Environment.CurrentDirectory, ".config", "config");

    public static string ProfilesConfigPath = Path.Combine(Environment.CurrentDirectory, ".config", "Profiles");

    public static async ValueTask<LocalConfig?> ReadConfigAsync()
    {
        var readStrean = File.OpenRead(LocalConfigPath);
        try
        {
            return await MemoryPackSerializer.DeserializeAsync<LocalConfig>(readStrean);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            await readStrean.DisposeAsync();
        }
    }

    public static async ValueTask WriteConfigAsync(LocalConfig localConfig)
    {
        var writeStream = File.OpenWrite(LocalConfigPath);
        try
        {
            await MemoryPackSerializer.SerializeAsync(writeStream, localConfig);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            await writeStream.DisposeAsync();
        }
    }
}