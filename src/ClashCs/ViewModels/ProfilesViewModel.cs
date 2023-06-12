using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using ClashCs.Tool;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClashCs.ViewModels;

public partial class ProfilesViewModel : ObservableObject
{
    
    [ObservableProperty]
    private string profilesLink;

    [RelayCommand]
    public async Task Download()
    {
        if (!string.IsNullOrEmpty(ProfilesLink))
        {
            var httpClient = Application.Current.GetService<IHttpClientFactory>();
            var client = httpClient.CreateClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd(Util.UA);
            var uri = new Uri(ProfilesLink.Trim());
            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var filename = response.Content.Headers.ContentDisposition.FileName;
            var subInfoExists = response.Headers.TryGetValues("Subscription-Userinfo", out var subInfo);
            var updateIntervalExists = response.Headers.TryGetValues("Profile-Update-Interval", out var updateInterval);
            var urlExists = response.Headers.TryGetValues("profile-web-page-url", out var url);

            var yaml = await response.Content.ReadAsStringAsync();
            
            //var profileInfo = Util.Deserializer<Config>(yaml);
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            var stringBuilder = new StringBuilder(13);
            stringBuilder.Append(timestamp).Append(".yaml");
            
            
        }
    }

    [RelayCommand]
    public async Task UpdateAll()
    {
        
    }
}