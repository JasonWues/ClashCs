using System;

namespace ClashCs.Config;

public class LazyConfig
{
    public readonly static Lazy<LazyConfig> Instance = new Lazy<LazyConfig>(() => new LazyConfig());

    public LazyConfig()
    {
        LocalConfig = new LocalConfig();
    }

    public LocalConfig LocalConfig { get; private set; }

    public void SetLocalConfig(LocalConfig localConfig)
    {
        LocalConfig = localConfig;
    }
}