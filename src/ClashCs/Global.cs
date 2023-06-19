using System;
using System.IO;

namespace ClashCs;

public static class Global
{

    public const string Loopback = "127.0.0.1";

    public const string UA = "ClashCs";
    
    public const string AutoRunRegPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

    public static string LocalConfigDicPath => Path.Join(Environment.CurrentDirectory, "Config");

    public static string LocalConfigPath => Path.Join(Environment.CurrentDirectory, "Config", "localConfig");

    public static string ProfilesDicPath => Path.Join(Environment.CurrentDirectory, "Config", "Profiles");

    public static string CorePath => Path.Join(Environment.CurrentDirectory, "Core");
}