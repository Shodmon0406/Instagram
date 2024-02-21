using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.Post;

[Index("ApplicationUserId", "PostLikeId", IsUnique = true)]
public class PostUserLike
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public int PostLikeId { get; set; }
    public PostLike PostLike { get; set; } = null!;
}