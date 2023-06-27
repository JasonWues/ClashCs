using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using FluentAvalonia.Styling;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Media;
using FluentAvalonia.UI.Windowing;

namespace ClashCs.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Application.Current!.ActualThemeVariantChanged += ApplicationActualThemeVariantChanged;
    }

    private void ApplicationActualThemeVariantChanged(object? sender, EventArgs e)
    {
        if (OperatingSystem.IsWindows())
        {
            if (IsWindows11 && ActualThemeVariant != FluentAvaloniaTheme.HighContrastTheme)
            {
                TryEnableMicaEffect();
            }
            else if (ActualThemeVariant != FluentAvaloniaTheme.HighContrastTheme)
            {
                SetValue(BackgroundProperty, AvaloniaProperty.UnsetValue);
            }
        }
    }


    protected override void OnLoaded()
    {
        base.OnLoaded();
        MainNav.MenuItemsSource = GetNavigationViewItems();
        FrameView.Navigate(typeof(DashboardView));
    }

    private void MainNav_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        var view = sender as NavigationView;
        if (e.SelectedItemContainer is NavigationViewItem item)
        {
            view.Header = item.Tag.ToString();
            switch (item.Tag)
            {
                case "Dashboard":
                    FrameView.Navigate(typeof(DashboardView));
                    break;
                case "Profiles":
                    FrameView.Navigate(typeof(ProfilesView));
                    break;
                case "设置":
                    FrameView.Navigate(typeof(SettingView));
                    break;
            }

        }
    }
    
    private List<NavigationViewItem> GetNavigationViewItems()
    {
        return new List<NavigationViewItem>
        {
            new NavigationViewItem
            {
                Content = "Home",
                Tag = "Dashboard",
            },
            new NavigationViewItem
            {
                Content = "Proxies",
                Tag = "Proxies",
            },
            new NavigationViewItem
            {
                Content = "Profiles",
                Tag = "Profiles",
            },
            new NavigationViewItem
            {
                Content = "Log",
                Tag = "Log",
            },
        };
    }

    private void TryEnableMicaEffect()
    {
        if (ActualThemeVariant == ThemeVariant.Dark)
        {
            var color = this.TryFindResource("SolidBackgroundFillColorBase",
                ThemeVariant.Dark, out var value)
                ? (Color2)(Color)value
                : new Color2(32, 32, 32);

            color = color.LightenPercent(-0.8f);

            Background = new ImmutableSolidColorBrush(color, 0.78);
        }
        else if (ActualThemeVariant == ThemeVariant.Light)
        {
            // Similar effect here
            var color = this.TryFindResource("SolidBackgroundFillColorBase",
                ThemeVariant.Light, out var value)
                ? (Color2)(Color)value
                : new Color2(243, 243, 243);

            color = color.LightenPercent(0.5f);

            Background = new ImmutableSolidColorBrush(color, 0.9);
        }
    }

    private void Window_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}