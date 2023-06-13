using System;
using System.IO;
using System.Threading.Tasks;
using ClashCs.Config;
using MemoryPack;

namespace ClashCs.Tool;

public class Util
{
    public readonly static Lazy<Util> Instance = new(() => new Util());

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