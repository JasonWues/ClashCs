using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ClashCs.Config;
using ClashCs.Tool;

namespace ClashCs.CoreFoundation;

public class CoreConfigManager
{
    public async Task GenerateClientConfig(ProfileItem profileItem)
    {
        if (profileItem == null)
        {
            return;
        }

        try
        {
            var address = profileItem.Address;
            if (string.IsNullOrEmpty(address))
            {
                return;
            }
            if (!File.Exists(address))
            {
                return;
            }
            var yaml = await File.ReadAllTextAsync(address);

            var fileContent = Util.Instance.Value.Deserializer<Dictionary<string,object>>(yaml);
            if (fileContent == null)
            {
                return;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}