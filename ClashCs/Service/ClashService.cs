using System.Net.Http.Headers;
using ClashCs.Interface;

namespace ClashCs;

public class ClashService : IClashService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ClashService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> Logs(string level)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
        return await Task.FromResult("sss");
    }

    public async Task<List<Entity.Config>> Config()
    {
        return null;
    }
}