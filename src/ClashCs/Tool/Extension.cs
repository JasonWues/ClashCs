using Avalonia;
using Microsoft.Extensions.DependencyInjection;

namespace ClashCs.Tool;

public static class Extension
{
    public static T GetService<T>(this Application? application) where T : class
    {
        return (application as App)!.Services.GetService<T>()!;
    }
}