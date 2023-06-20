using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Threading;
using ClashCs.Config;
using ClashCs.Model;
using ClashCs.Tool;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClashCs.ViewModels;

public partial class ProfilesViewModel : ObservableObject
{
    readonly private LocalConfig localConfig;
    
    public ProfilesViewModel()
    {
        localConfig = LazyConfig.Instance.Value.LocalConfig;
        ProfileItems.AddRange(localConfig.ProfileItems);
    }
    
    [ObservableProperty]
    private ObservableCollection<ProfileItem> profileItems = new ObservableCollection<ProfileItem>();
    
    [ObservableProperty]
    private string profilesLink;

    [RelayCommand]
    private async Task DownloadAsync()
    {
        try
        {
            if (!string.IsNullOrEmpty(ProfilesLink))
            {
                var httpClient = Application.Current.GetService<IHttpClientFactory>();
                var client = httpClient.CreateClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd(Global.UA);
                var uri = new Uri(ProfilesLink.Trim());
                var response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var filename = response.Content.Headers.ContentDisposition?.FileName;
                var subInfoExists = response.Headers.TryGetValues("Subscription-Userinfo", out var subInfo);
                var updateIntervalExists = response.Headers.TryGetValues("Profile-Update-Interval", out var updateInterval);
                var homeUrlExists = response.Headers.TryGetValues("profile-web-page-url", out var homeUrl);

                var yaml = await response.Content.ReadAsStringAsync();
                
                var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                var stringBuilder = new StringBuilder(13);
                stringBuilder.Append(timestamp).Append(".yaml");
                if (!Directory.Exists(Global.ProfilesDicPath))
                {
                    Directory.CreateDirectory(Global.ProfilesDicPath);
                }

                var subInfoDic = subInfo != null && subInfo.Any() ? subInfo.FirstOrDefault().Split(';')
                    .ToDictionary(x => x.Split('=')?[0]?.Trim(), x => x.Split('=')?[1]) : new Dictionary<string, string>();
                
                var address = Path.Join(Global.ProfilesDicPath, stringBuilder.ToString());
                
                
                var downloadExists = subInfoDic.TryGetValue("download", out var download);
                var totalExists = subInfoDic.TryGetValue("total", out var total);
                var expireExists = subInfoDic.TryGetValue("expire", out var expire);
                

                await File.WriteAllTextAsync(address, yaml, Encoding.UTF8);

                ProfileItem profileItem = new ProfileItem
                {
                    Url = ProfilesLink,
                    HomeUrl = homeUrlExists ? homeUrl.FirstOrDefault() : null,
                    Address = address,
                    FileName = filename,
                    IndexId = Guid.NewGuid(),
                    Expire = expireExists ? DateTimeOffset.FromUnixTimeSeconds(long.Parse(expire)) : DateTimeOffset.MinValue,
                    Download = downloadExists ? $"{ulong.Parse(download) / 1024 / 1024 / 1024}G" : "0",
                    Total = totalExists ? $"{ulong.Parse(total) / 1024 / 1024 / 1024}G" : "0"
                };
                
                localConfig.ProfileItems.Add(profileItem);

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    ProfileItems.Add(profileItem);
                });

                await Util.Instance.Value.SaveConfigAsync(localConfig);
            }
            else
            {
                
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    [RelayCommand]
    public async Task UpdateAllAsync()
    {

    }

    [RelayCommand]
    private void DeleteProfile()
    {
        
    }

    [RelayCommand]
    private void ShowInFolder()
    {
        var path = "Test";
        if (OperatingSystem.IsWindows())
        {
            using Process fileOpener = new Process();
            fileOpener.StartInfo.FileName = "explorer";
            fileOpener.StartInfo.Arguments = $"/select, {path}";
            fileOpener.Start();
        }
        else if (OperatingSystem.IsMacOS())
        {
            using Process fileOpener = new Process();
            fileOpener.StartInfo.FileName = "explorer";
            fileOpener.StartInfo.Arguments = $"-R {path}";
            fileOpener.Start();
        }
        else if (OperatingSystem.IsLinux())
        {
            using Process dbusShowItemsProcess = new Process();
            dbusShowItemsProcess.StartInfo = new ProcessStartInfo
            {
                FileName = "dbus-send",
                Arguments = "--print-reply --dest=org.freedesktop.FileManager1 /org/freedesktop/FileManager1 org.freedesktop.FileManager1.ShowItems array:string:\"file://"+ path +"\" string:\"\"",
                UseShellExecute = true
            };
            dbusShowItemsProcess.Start();
        }
        else
        {
            using Process fileOpener = new Process();
            fileOpener.StartInfo.FileName = Path.GetDirectoryName(path);
            fileOpener.StartInfo.UseShellExecute = true;
            fileOpener.Start();
        }
    }
}