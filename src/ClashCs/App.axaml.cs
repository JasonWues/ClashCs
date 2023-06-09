using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ClashCs.Config;
using ClashCs.CoreFoundation;
using ClashCs.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ClashCs;

public class App : Application
{
    public IServiceProvider Services { get; private set; } = null!;

    public TrayIcon TrayIcon { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        if (OperatingSystem.IsMacOS() && !OperatingSystem.IsMacOSVersionAtLeast(10, 13))
        {
            Environment.Exit(0);
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow();
            desktop.MainWindow.MinWidth = 1024;
            desktop.MainWindow.MinHeight = 576;
            ConfigTray();
        }

        base.OnFrameworkInitializationCompleted();
        await ConfigManager.LoadConfig();
    }


    public override void RegisterServices()
    {
        base.RegisterServices();
        Services = ConfigureServices();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddHttpClient();
        services.AddSingleton<CoreConfigManager>();
        services.AddSingleton<CoreManager>();
        return services.BuildServiceProvider();
    }

    private void ConfigTray()
    {
        TrayIcon = new TrayIcon();
        TrayIcon.Menu ??= new NativeMenu();
        TrayIcon.ToolTipText = "ClashCs";
        TrayIcon.Clicked += (sender, args) =>
        {
            var desktop = ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            desktop.MainWindow.Show();
        };
    }
}