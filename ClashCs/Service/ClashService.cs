using System.Net.Http.Headers;
using ClashCs.Server.Interface;

namespace ClashCs.Server.Service;

public class ClashService : IClashService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public ClashService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetLogs(string level)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
        return await Task.FromResult("sss");
    }
}