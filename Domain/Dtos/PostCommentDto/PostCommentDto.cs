using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.PostCommentDto;

public class PostCommentDto
{
    [Required]
    [MaxLength(300)]
    public string Comment { get; set; } = null!;
}