using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.PostCommentDto;

public class LikeCommentPostDto
{
    [Required]
    [MaxLength(50)]
    public string UserId { get; set; } = null!;
    [Required]
    public int PostId { get; set; }
}
