namespace Db.Models;

public class Podcast
{
    public required int Id { get; init; }
    public required string FeedId { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
}