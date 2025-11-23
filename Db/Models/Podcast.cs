namespace Db.Models;

public class Podcast
{
    public required int Id { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    
    public required string SourceId { get; init; }
    public Source? Source { get; init; }
}