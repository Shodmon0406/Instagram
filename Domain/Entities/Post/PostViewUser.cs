using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities.Post;

public class PostViewUser
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public int PostViewId { get; set; }
    public PostView PostView { get; set; } = null!;
}