namespace ClashCs.Interface;

public interface IClashService
{
    Task<string> Logs(string level);

    Task<List<Entity.Config>> Config();
}