using Avalonia.Controls;
using Avalonia.Interactivity;
using FluentAvalonia.UI.Controls;

namespace ClashCs.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    private void MainNav_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItemContainer is NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "Home":
                    FrameView.Navigate(typeof(HomeView));
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

    private void MainNav_OnLoaded(object? sender, RoutedEventArgs e)
    {
        MainNav.SelectedItem = MainNav.MenuItems[0];
    }
}