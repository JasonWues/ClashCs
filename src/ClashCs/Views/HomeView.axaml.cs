﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ClashCs.ViewModels;

namespace ClashCs.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}