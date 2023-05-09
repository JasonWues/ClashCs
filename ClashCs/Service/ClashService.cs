using System.Net.Http.Headers;
using ClashCs.Config;
using ClashCs.Interface;

namespace ClashCs;

public class ClashService : IClashService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClashService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> LogsAsync(string level)
    {
        return await Task.FromResult("sss");
    }

    public List<LocalProxyConfig> LocalProxyConfigs()
    {
        return GlobalConfig.LocalConfig.LocalProxyConfigs;
    }
}