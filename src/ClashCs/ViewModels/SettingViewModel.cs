using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClashCs.ViewModels;

public partial class SettingViewModel : ObservableObject
{
    [ObservableProperty]
    private bool autoRun;

    partial void OnAutoRunChanged(bool value)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void SetAutoRun()
    {
        
    }
    
}