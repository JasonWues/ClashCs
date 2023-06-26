using System;
using MemoryPack;

namespace ClashCs.Config;

[MemoryPackable]
public partial class ProfileItem
{
    public string FileName { get; set; }

    public string Address { get; set; }

    public string Url { get; set; }

    public string HomeUrl { get; set; }

    public long UpdateTime { get; set; }

    public ulong Upload { get; set; }

    public ulong Download { get; set; }

    public ulong Total { get; set; }

    public DateTimeOffset Expire { get; set; }
    
    public bool IsActive { get; set; }
    
}