namespace ClashCs.Interface;

public interface IClashService
{
    Task<string> LogsAsync(string level);

    List<Entity.Config> Config();
}