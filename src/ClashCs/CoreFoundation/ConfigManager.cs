using System.IO;
using System.Threading.Tasks;
using Avalonia;
using ClashCs.Config;
using ClashCs.Model;
using ClashCs.Tool;

namespace ClashCs.CoreFoundation;

public static class ConfigManager
{
    public static async Task LoadConfig()
    {
        var util = Util.Instance.Value;
        var localConfig = LazyConfig.Instance.Value.LocalConfig;
        var exists = File.Exists(Global.LocalConfigPath);
        if (!exists)
        {
            if (!Directory.Exists(Global.LocalConfigDicPath))
            {
                Directory.CreateDirectory(Global.LocalConfigDicPath);
            }

            await util.SaveConfigAsync(localConfig);
        }
        else
        {
            LazyConfig.Instance.Value.SetLocalConfig(await util.ReadConfigAsync());
        }
    }
}