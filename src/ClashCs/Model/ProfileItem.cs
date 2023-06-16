using System;

namespace ClashCs.Model;

public class ProfileItem
{
    public Guid IndexId { get; set; }
    
    public string Address { get; set; }
    
    public string Url { get; set; }
    
    public long UpdateTime { get; set; }
    
    public ulong Upload { get; set; }
    
    public ulong Download { get; set; }
    
    public ulong Total { get; set; }
    
    public long Expire { get; set; }
}