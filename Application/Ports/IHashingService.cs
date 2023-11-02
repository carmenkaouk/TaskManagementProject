namespace Application.Ports;

public interface IHashingService
{
    public string Hash(string toHash);
}
