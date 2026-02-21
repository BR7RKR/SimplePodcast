namespace Db;

public interface IPagination<TEntity, out TPredicate>
{
    public Task<IEnumerable<TEntity>> GetManyAsync(
        Action<TPredicate>? predicate = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancel = default
    );
}