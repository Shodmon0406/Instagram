using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Post;

public class PostComment
{
    public int PostCommentId { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
    public User.ApplicationUser ApplicationUser { get; set; } = null!;
    public string Comment { get; set; } = null!;
    public DateTime DateCommented { get; set; }
    public PostCommentLike PostCommentLike { get; set; } = null!;
}