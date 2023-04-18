namespace ClashCs.Config;

public class ProxyConfig
{
    public Entity.Config BaseConfig { get; set; } = new Entity.Config();
    public List<Entity.Config> Configs { get; set; } = new List<Entity.Config>();
}