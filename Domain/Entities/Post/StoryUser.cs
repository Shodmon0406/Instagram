using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Post;
public class StoryUser
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public int StoryId { get; set; }
    
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public Story Story { get; set; } = null!;
}