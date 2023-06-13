using System;

namespace ClashCs.Config;

public class LazyConfig
{
    public readonly static Lazy<LazyConfig> Instance = new(() => new LazyConfig());

    public LocalConfig Config { get; private set; }

    public void SetConfig(LocalConfig config)
    {
        Config = config;
    }
}