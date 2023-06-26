using System.Threading.Tasks;
using Avalonia;
using ClashCs.CoreFoundation;
using ClashCs.Tool;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace ClashCs.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    readonly private CoreManager coreManager;
    
    public DashboardViewModel()
    {
        coreManager = Application.Current.GetService<CoreManager>();
    }
    
    [RelayCommand]
    public async Task LoadCoreAsync()
    {
        await coreManager.LoadCoreAsync();
    }
}