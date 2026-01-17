using Db.Models;

namespace Core;

public class SourceData : ISourceData
{
    public int Id { get; init; }
    public required string Url { get; init; }
    
    public required SourceType Type { get; init; }
}