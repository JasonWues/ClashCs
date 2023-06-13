using System.IO;
using ClashCs.Config;
using ClashCs.Tool;

namespace ClashCs.CoreFoundation;

public static class ConfigManager
{
    public static void LoadConfig()
    {
        var util = Util.Instance.Value;
        var lazyConfig = LazyConfig.Instance.Value;
        var exists = Directory.Exists(Global.LocalConfigDicPath);
        if (!exists)
        {
            Directory.CreateDirectory(Global.LocalConfigDicPath);
            File.Create(Global.LocalConfigPath).Dispose();
            var localConfig = new LocalConfig();
            util.SaveConfig(localConfig);
            lazyConfig.SetConfig(localConfig);
        }
        else
        {
            lazyConfig.SetConfig(util.ReadConfigAsync().GetAwaiter().GetResult());
        }


    }
}