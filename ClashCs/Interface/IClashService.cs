using ClashCs.Config;

namespace ClashCs.Interface;

public interface IClashService
{
    Task<string> LogsAsync(string level);

    List<LocalProxyConfig> LocalProxyConfigs();
}