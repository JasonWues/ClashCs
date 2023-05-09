﻿using ClashCs.Config;
using MemoryPack;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using ClashCs.Entity;

namespace ClashCs;

public static class Util
{
    private static readonly IDeserializer DeserializerInstead = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties()
        .Build();

    public static T Deserializer<T>(string input) where T : new()
    {
        if (!string.IsNullOrEmpty(input))
        {
            return DeserializerInstead.Deserialize<T>(input);
        }

        return new T();
    }

    public static T Deserializer<T>(TextReader input)
    {
        return DeserializerInstead.Deserialize<T>(input);
    }

    public static readonly string UA =
        "ClashCs";

    public static readonly string LocalConfigPath = Path.Join(Environment.CurrentDirectory, ".config", "config");

    public static readonly string ProfilesConfigPath = Path.Join(Environment.CurrentDirectory, ".config", "Profiles");

    public static readonly string CorePath = Path.Join(Environment.CurrentDirectory, "Core");

    public static string ClashConfig { get => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); }

    public static string ClashEnumToString(ClashEnum clashEnum){
        return clashEnum switch{
            ClashEnum.Clash => "clash",
            ClashEnum.Clash_Premium => "clash-premium-bin",
            ClashEnum.Clash_Meta => "clash-meta",
            _ => ""
        };
    }

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