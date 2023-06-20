using System;

namespace ClashCs.Config;

public class LazyConfig
{
    public readonly static Lazy<LazyConfig> Instance = new Lazy<LazyConfig>(() => new LazyConfig());

    public LazyConfig()
    {
        LocalConfig = new LocalConfig();
    }
    public void SetLocalConfig(LocalConfig localConfig)
    {
        LocalConfig = localConfig;
    }

    public LocalConfig LocalConfig { get; private set; }
}