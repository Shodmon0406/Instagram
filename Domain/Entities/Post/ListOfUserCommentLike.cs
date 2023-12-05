using Domain.Entities.User;

namespace Domain.Entities.Post;

public class ListOfUserCommentLike
{
    public int Id { get; set; }
    public int PostCommentLikeId { get; set; }
    public PostCommentLike PostCommentLike { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
}