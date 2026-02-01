namespace Db;

public interface IDbContext
{
    public string Connection { get; }
    
    public void Start();
}