using Db.Models;

namespace Db;

public class SourcePredicate
{
    public int? Id { get; set; }
    public string? Url { get; set; }
    public SourceType? Type { get; set; }
}

public interface ISourcesRepository : ICrud<int, Source>, IPagination<Source, SourcePredicate>
{
    
}