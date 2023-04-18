using MemoryPack;

namespace ClashCs.Config;

[MemoryPackable]
public partial class LocalConfig
{
    public bool IsBoot { get; set; } = false;

    public Dictionary<long, string> FileAlias { get; set; } = new Dictionary<long, string>();
}