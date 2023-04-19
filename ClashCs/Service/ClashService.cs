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
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer ", GlobalConfig.ProxyConfig.BaseConfig.Secret);
        return await Task.FromResult("sss");
    }

    public List<Entity.Config> Config()
    {
        return GlobalConfig.ProxyConfig.Configs;
    }
}