using System;
using System.IO;

namespace ClashCs;

public static class Global
{

    public const string Loopback = "127.0.0.1";

    public readonly static string UA = "ClashCs";

    public static string LocalConfigDicPath => Path.Join(Environment.CurrentDirectory, "Config");

    public static string LocalConfigPath => Path.Join(Environment.CurrentDirectory, "Config", "localConfig");
}