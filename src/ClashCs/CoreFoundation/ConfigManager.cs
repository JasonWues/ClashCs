using System;
using System.IO;
using ClashCs.Tool;

namespace ClashCs.CoreFoundation;

public static class ConfigManager
{
    public static void LoadConfig()
    {
        try
        {
            var exists = Directory.Exists(Global.LocalConfigDicPath);
            if (!exists)
            {
                Directory.CreateDirectory(Global.LocalConfigDicPath);
                File.Create(Global.LocalConfigPath);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}