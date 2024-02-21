using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.PostViewUserDto;

public class PostViewUserDto
{
    [MaxLength(50)]
    public int PostViewId { get; set; }
    public string UserId { get; set; } = null!;
}