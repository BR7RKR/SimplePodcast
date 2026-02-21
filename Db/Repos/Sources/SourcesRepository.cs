using System.Data;
using System.Text;
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

    public async Task<IEnumerable<Source>> GetManyAsync(
        Action<SourcePredicate>? predicate = null,
        int? skip = null, 
        int? take = null, 
        CancellationToken cancel = default
    )
    {
        using IDbConnection db = new SqliteConnection(_dbContext.Connection);

        var sb = new StringBuilder($"SELECT * FROM {Source.TableName}");
        var parameters = new DynamicParameters();
        
        if (predicate is not null)
        {
            var sourcePredicate = new SourcePredicate();
            predicate.Invoke(sourcePredicate);
            
            var conditions = new List<string>();

            if (sourcePredicate.Id is not null)
            {
                conditions.Add($"{nameof(Source.Id)} = @Id");
                parameters.Add("Id", sourcePredicate.Id);
            }

            if (sourcePredicate.Url is not null)
            {
                conditions.Add($"{nameof(Source.Url)} = @Url");
                parameters.Add("Url", sourcePredicate.Url);
            }

            if (sourcePredicate.Type is not null)
            {
                conditions.Add($"{nameof(Source.Type)} = @Type");
                parameters.Add("Type", sourcePredicate.Type);
            }

            if (conditions.Count > 0)
            {
                sb.Append($" WHERE {string.Join(" AND ", conditions)}");
            }
        }
        
        if (skip is not null)
        {
            sb.Append(" LIMIT -1 OFFSET @Skip");
            parameters.Add("Skip", skip);
        }
        
        if (take is not null)
        {
            if (skip is null)
            {
                sb.Append(" LIMIT @Take");
            }
            else
            {
                sb.Replace("LIMIT -1", "LIMIT @Take");
                parameters.Add("Take", take);
            }
        }
        
        var cmd = new CommandDefinition(sb.ToString(), parameters, cancellationToken: cancel);
        var result = await db.QueryAsync<Source>(cmd);
        return result;
    }
}