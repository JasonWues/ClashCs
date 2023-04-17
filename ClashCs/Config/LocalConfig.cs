using MemoryPack;

namespace ClashCs.Config;

[MemoryPackable]
public partial class LocalConfig
{
    public bool IsBoot { get; set; } = false;
    
    public Dictionary<int,string> FileAlias { get; set; }
}