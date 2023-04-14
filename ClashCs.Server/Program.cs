using MudBlazor.Services;
using System.Diagnostics;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});

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
        process.StartInfo.FileName = Path.Combine(Environment.CurrentDirectory, "Core", "Clash", "clash-windows-amd64-v3.exe");
    }

    process.Start();
}

bool CheckClashConfig()
{
    if (OperatingSystem.IsLinux())
    {
        var homePath = Environment.GetEnvironmentVariable("$HOME");
        if (!string.IsNullOrEmpty(homePath))
        {
            return File.Exists(Path.Combine(homePath, ".config", "clash", "config.yaml"));
        }
    }
    else if (OperatingSystem.IsWindows())
    {
        var exists = File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "clash", "config.yaml"));
        if (exists)
        {

        }
    }
    return false;
}