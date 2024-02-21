using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.PostDto;

public class PostDto
{
    [MaxLength(50)]
    public string? Title { get; set; }
    [MaxLength(1000)]
    public string? Content { get; set; }
    [Required]
    public List<IFormFile> Images { get; set; } = null!;
}