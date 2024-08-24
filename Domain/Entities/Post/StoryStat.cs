namespace Domain.Entities.Post;

public class StoryStat
{
    public int Id { get; set; }
    public int ViewCount { get; set; }
    public int ViewLike { get; set; }
    public int StoryId { get; set; }
    public Story Story { get; set; } = null!;
}