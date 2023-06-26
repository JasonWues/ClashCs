using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ClashCs.Config;
using MemoryPack;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClashCs.Tool;

public class Util
{
    public readonly static Lazy<Util> Instance = new Lazy<Util>(() => new Util());

    public Util()
    {
        var context = new ClashYamlContext();
        DeserializerInstead = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
    }

    private IDeserializer DeserializerInstead { get; }

    public bool IsAdministrator()
    {
        if (OperatingSystem.IsWindows())
        {
            try
            {
                var current = WindowsIdentity.GetCurrent();
                WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
                return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        return true;
    }

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

    public async Task<string> GetClipboardData()
    {
        var mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;
        
        var toplevel = TopLevel.GetTopLevel(mainWindow);
        return await toplevel.Clipboard.GetTextAsync();
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

    public byte[] SaveConfig(LocalConfig localConfig)
    {
        try
        {
            return MemoryPackSerializer.Serialize(localConfig);
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
    

    public LocalConfig ReadConfig()
    {
        try
        {
            var bytes = File.ReadAllBytes(Global.LocalConfigPath);
            return MemoryPackSerializer.Deserialize<LocalConfig>(bytes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}