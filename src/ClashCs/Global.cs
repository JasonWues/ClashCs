using System;
using System.IO;

namespace ClashCs;

public static class Global
{
    public static string LocalConfigDicPath => Path.Join(Environment.CurrentDirectory, ".config");

    public static string LocalConfigPath => Path.Join(Environment.CurrentDirectory, ".config","config");
    
    public const string Loopback = "127.0.0.1";
}