using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TagDto;

public class TagDto
{
    [MaxLength(50)]
    public string TagName { get; set; } = null!;
}