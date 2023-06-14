using System;
using System.IO;
using System.Threading.Tasks;
using ClashCs.Config;
using MemoryPack;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClashCs.Tool;

public class Util
{
    public readonly static Lazy<Util> Instance = new(() => new Util());

    public Util()
    {
        var context = new ClashYamlContext();
        DeserializerInstead = new StaticDeserializerBuilder(context)
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
    }
    
    private IDeserializer DeserializerInstead { get; set; }
    
    public T Deserializer<T>(string input) where T : new()
    {
        try
        {
            if (!string.IsNullOrEmpty(input))
            {
                return DeserializerInstead.Deserialize<T>(input);
            }

            return new T();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async ValueTask SaveConfigAsync(LocalConfig localConfig)
    {
        try
        {
            await using var writeStream = File.OpenWrite(Global.LocalConfigPath);
            await MemoryPackSerializer.SerializeAsync(writeStream, localConfig);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void SaveConfig(LocalConfig localConfig)
    {
        try
        {
            MemoryPackSerializer.Serialize(localConfig);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async ValueTask<LocalConfig?> ReadConfigAsync()
    {
        try
        {
            await using var readStrean = File.OpenRead(Global.LocalConfigPath);
            return await MemoryPackSerializer.DeserializeAsync<LocalConfig>(readStrean);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}