using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ClashCs.Views;

public partial class SettingView : UserControl
{
    public SettingView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}