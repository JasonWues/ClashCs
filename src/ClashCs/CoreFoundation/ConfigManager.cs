using System.IO;
using ClashCs.Config;
using ClashCs.Tool;

namespace ClashCs.CoreFoundation;

public static class ConfigManager
{
    public static void LoadConfig()
    {
        var util = Util.Instance.Value;
        var localConfig = App.Current.GetService<LocalConfig>();
        var exists = Directory.Exists(Global.LocalConfigDicPath);
        if (!exists)
        {
            Directory.CreateDirectory(Global.LocalConfigDicPath);
            File.Create(Global.LocalConfigPath).Dispose();
            util.SaveConfig(localConfig);
        }
        else
        {
            localConfig = util.ReadConfigAsync().GetAwaiter().GetResult();
        }


    }
}