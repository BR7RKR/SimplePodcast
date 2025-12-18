namespace Db.Models;

public enum SourceType
{
    Rss,
    Youtube
}

public class Source
{
    public required int Id { get; init; }
    public required string Url { get; init; }
    public required SourceType Type { get; init; }
}