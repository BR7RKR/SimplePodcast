using System.Data;
using Dapper;
using Db.Models;
using Microsoft.Data.Sqlite;

namespace Db;

public sealed class SourcesRepository : ISourcesRepository
{
    private readonly IDbContext _dbContext;
    
    public SourcesRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateAsync(Source source, CancellationToken cancel = default)
    {
        using IDbConnection db = new SqliteConnection(_dbContext.Connection);

        var query =
            $"INSERT INTO {Source.TableName} ({nameof(Source.Url)}, {nameof(Source.Type)}) VALUES (@Url, @Type)";
        var cmd = new CommandDefinition(query, source, cancellationToken: cancel);
        
        await db.ExecuteAsync(cmd);
    }
    
    public async Task UpdateAsync(int id, Source source, CancellationToken cancel = default)
    {
        using IDbConnection db = new SqliteConnection(_dbContext.Connection);

        var query =
            $"UPDATE {Source.TableName} SET {nameof(Source.Url)} = @Url, {nameof(Source.Type)} = @Type WHERE {nameof(Source.Id)} = @Id";
        var cmd = new CommandDefinition(query, source, cancellationToken: cancel);
        
        await db.ExecuteAsync(cmd);
    }

    public async Task DeleteAsync(int id, CancellationToken cancel = default)
    {
        using IDbConnection db = new SqliteConnection(_dbContext.Connection);

        var query = $"DELETE FROM {Source.TableName} WHERE {nameof(Source.Id)} = @Id";
        var cmd = new CommandDefinition(query, parameters: new { Id = id }, cancellationToken: cancel);
        
        await db.ExecuteAsync(cmd);
    }

    public async Task<Source?> GetAsync(int id, CancellationToken cancel = default)
    {
        using IDbConnection db = new SqliteConnection(_dbContext.Connection);
        
        var query = $"SELECT * FROM {Source.TableName} WHERE {nameof(Source.Id)} = @Id";
        var cmd = new CommandDefinition(query, new {Id = id}, cancellationToken: cancel);
        
        var result = await db.QueryFirstOrDefaultAsync<Source>(cmd);
        return result;
    }

    public Task<IEnumerable<Source>> GetManyAsync(
        Action<Source>? predicate = null,
        int? skip = null, 
        int? take = null, 
        CancellationToken cancel = default
    )
    {
        throw new NotImplementedException();
    }
}