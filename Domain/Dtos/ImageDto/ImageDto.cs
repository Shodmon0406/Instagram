using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ImageDto;

public class ImageDto
{
    public int ImageId { get; set; }
    public int PostId { get; set; }
    [MaxLength(50)]
    public string Path { get; set; } = null!;
}