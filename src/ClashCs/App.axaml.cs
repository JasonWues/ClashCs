using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ClashCs.CoreFoundation;
using ClashCs.Views;
using Microsoft.Extensions.DependencyInjection;

namespace ClashCs;

public class App : Application
{
    public IServiceProvider Services { get; private set; } = null!;

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
        return services.BuildServiceProvider();
    }

    private void ConfigTray()
    {
        var notifyIcon = new TrayIcon();
        notifyIcon.Menu ??= new NativeMenu();
        notifyIcon.ToolTipText = "ClashCs";
    }
}