using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities.Post;
public class StoryUser
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = null!;
    public int StoryId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public Story Story { get; set; } = null!;
}