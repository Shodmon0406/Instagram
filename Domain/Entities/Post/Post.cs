using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities.Post;

public class Post
{
    public int PostId { get; set; }
    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    [MaxLength(50)]
    public string? Title { get; set; }
    [MaxLength(1000)]
    public string? Content { get; set; }
    public DateTime DatePublished { get; set; }
    public List<Story> Stories { get; set; } = null!;
    public List<PostComment> PostComments { get; set; } = null!;
    public PostFavorite PostFavorite { get; set; } = null!;
    public PostView PostView { get; set; } = null!;
    public PostLike PostLike { get; set; } = null!;
    public List<PostCategory> PostCategories { get; set; } = null!;
    public List<Image> Images { get; set; } = null!;
}