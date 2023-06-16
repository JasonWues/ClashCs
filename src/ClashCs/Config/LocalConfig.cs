using System.Collections.Generic;
using ClashCs.Model;
using MemoryPack;

namespace ClashCs.Config;

[MemoryPackable]
public partial class LocalConfig
{
    public bool IsBoot { get; set; } = false;

    public int MixedPort { get; set; } = 7888;

    public int HttpPort { get; set; } = 7890;

    public int SocksPort { get; set; } = 7891;
    
    public int ApiPort { get; set; } = 9090;

    public bool EnableIpv6 { get; set; }

    public bool AllowLanConn { get; set; }

    public bool EnableTun { get; set; }

    public List<ProfileItem> ProfileItems { get; set; } = new List<ProfileItem>();
    
}