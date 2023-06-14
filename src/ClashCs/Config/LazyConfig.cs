using System;

namespace ClashCs.Config;

public class LazyConfig
{
    public readonly static Lazy<LazyConfig> Instance = new(() => new LazyConfig());

    private LocalConfig Config { get; set; }

    public void SetConfig(LocalConfig config)
    {
        Config = config;
    }

    public LocalConfig GetConfig() => Config;
}