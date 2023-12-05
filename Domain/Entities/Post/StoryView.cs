using Domain.Entities.User;

namespace Domain.Entities.Post;

public class StoryView
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public int StoryId { get; set; }

    public Story Story { get; set; } = null!;
    
}