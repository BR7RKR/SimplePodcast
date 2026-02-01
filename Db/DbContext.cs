using FluentMigrator.Runner;

namespace Db;

public class DbContext : IDbContext
{
    public static string ConnectionString => 
        $"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "simplepodcast.db")}";
    
    public string Connection => ConnectionString;
    
    private readonly IMigrationRunner _migrationRunner;

    public DbContext(IMigrationRunner migrationRunner)  
    {
        _migrationRunner = migrationRunner;
    }

    public void Start()
    {
        _migrationRunner.MigrateUp();
    }
}