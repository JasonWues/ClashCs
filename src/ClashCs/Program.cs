using Avalonia;
using System;
using Avalonia.Logging;
using ClashCs.Util;

namespace ClashCs
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                BuildAvaloniaApp()
                    .StartWithClassicDesktopLifetime(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>() 
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .InitConfig()
                .InitClashConfig();
    }
}