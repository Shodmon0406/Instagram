using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.SearchHistoryDto;

public class SearchHistoryDto
{
    [Required]
    [MaxLength(50)]
    public string Text { get; set; } = null!;
}