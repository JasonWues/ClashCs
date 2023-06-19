using System.IO;
using System.Threading.Tasks;
using Avalonia;
using ClashCs.Config;
using ClashCs.Tool;

namespace ClashCs.CoreFoundation;

public static class ConfigManager
{
    public static async Task LoadConfig()
    {
        var util = Util.Instance.Value;
        var localConfig = Application.Current.GetService<LocalConfig>();
        var exists = File.Exists(Global.LocalConfigPath);
        if (!exists)
        {
            if (!Directory.Exists(Global.LocalConfigDicPath))
            {
                Directory.CreateDirectory(Global.LocalConfigDicPath);
            }
            File.Create(Global.LocalConfigPath).Dispose();
            await util.SaveConfigAsync(localConfig);
        }
        else
        {
            localConfig = await util.ReadConfigAsync();
        }


    }
}