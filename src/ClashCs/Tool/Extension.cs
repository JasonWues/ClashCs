﻿using System;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using ClashCs.CoreFoundation;

namespace ClashCs.Tool;

public static class Extension
{
    public static AppBuilder InitConfig(this AppBuilder builder)
    {
        ConfigManager.LoadConfig();
        return builder;
    }
    
    public static AppBuilder InitClashConfig(this AppBuilder builder)
    {
        if (Process.GetProcesses().ToList().Any(x =>
                x.ProcessName.Contains("clash", StringComparison.OrdinalIgnoreCase) && x.Id != Environment.ProcessId))
        {
#if DEBUG


#else
    
    //Console.WriteLine("Clash 已在运行");
    //Environment.Exit(1);

#endif
        }
        return builder;
    }
}