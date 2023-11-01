namespace Persistence.Interfaces;

public interface IDatabase
{
    public Task SaveChangesAsync();
}
