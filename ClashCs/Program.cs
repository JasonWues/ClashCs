using ClashCs;
using ClashCs.Config;
using ClashCs.Entity;
using ClashCs.Interface;
using MudBlazor.Services;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

if (Process.GetProcesses().ToList().Any(x =>
        x.ProcessName.Contains("clash", StringComparison.OrdinalIgnoreCase) && x.Id != Environment.ProcessId))
{
#if DEBUG

#else
    Console.WriteLine("Clash 已在运行");
    Environment.Exit(1);
#endif
}

await CheckLocalConfig();
await CheckClashConfigAsync();
CheckProxyConfig();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});
builder.Services.AddSingleton<IClashService, ClashService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseCors();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

void StartClash()
{
    Process process = new Process();

    if (Environment.OSVersion.Platform == PlatformID.Win32NT)
    {
        process.StartInfo.FileName =
            Path.Combine(Environment.CurrentDirectory, "Core", "Clash", "clash-windows-amd64-v3.exe");
    }

    process.Start();
}

async Task CheckClashConfigAsync()
{
    if (OperatingSystem.IsLinux())
    {
        var homePath = Environment.GetEnvironmentVariable("$HOME");
        if (!string.IsNullOrEmpty(homePath))
        {
            var path = Path.Combine(homePath, ".config", "clash", "config.yaml");
            var exists = File.Exists(path);
            if (exists)
            {
                GlobalConfig.ProxyConfig.BaseConfig =
                    Util.Deserializer<Config>(await File.ReadAllTextAsync(path));
            }
            else
            {
                await GenerateBaseConfig(path);
            }
        }
    }
    else if (OperatingSystem.IsWindows())
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "clash",
            "config.yaml");
        var exists = File.Exists(path);
        if (exists)
        {
            GlobalConfig.ProxyConfig.BaseConfig =
                Util.Deserializer<Config>(await File.ReadAllTextAsync(path));
        }
        else
        {
            await GenerateBaseConfig(path);
        }
    }
}

async Task CheckLocalConfig()
{
    var exists = File.Exists(Util.LocalConfigPath);
    if (!exists)
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        GlobalConfig.LocalConfig.LocalProxyConfigs = new List<LocalProxyConfig>(1)
        {
            new LocalProxyConfig()
            {
                FileName = $"{timestamp}.yaml",
                SubscriptionName = "default",
                Url = null,
                HomeLink = null,
                Selected = null,
                SubInfo = null
            }
        };

        await Util.WriteConfigAsync(GlobalConfig.LocalConfig);
    }
    else
    {
        GlobalConfig.LocalConfig = await Util.ReadConfigAsync();
    }
}

void CheckProxyConfig()
{
    DirectoryInfo root = new DirectoryInfo(Util.ProfilesConfigPath);
    var files = root.GetFiles("*.yaml");
    if (files.Any())
    {
        foreach (var fileInfo in files)
        {
            using var reader = fileInfo.OpenText();
            GlobalConfig.ProxyConfig.Configs.Add(Util.Deserializer<Config>(reader));
        }
    }
}

async Task GenerateBaseConfig(string path)
{
    int port = 0;
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    try
    {
        IPEndPoint localEP = new IPEndPoint(IPAddress.Any, 0);
        socket.Bind(localEP);
        localEP = (IPEndPoint)socket.LocalEndPoint!;
        port = localEP.Port;

        var yaml = $"""
mixed-port: 7890
allow-lan: false
external-controller: 127.0.0.1:{port}
secret: ffdeb845-2700-4fd4-8b53-a252df25ce71
""";
        path = Path.Combine(path, "config.yaml");
        File.Create(path);
        await File.WriteAllTextAsync(path, yaml, Encoding.UTF8);
        GlobalConfig.ProxyConfig.BaseConfig = Util.Deserializer<Config>(yaml);
    }
    finally
    {
        socket.Close();
    }
}