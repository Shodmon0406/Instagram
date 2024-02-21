using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.MessageDto;

public class MessageDto
{
    [Required]
    public int ChatId { get; set; }
    [Required]
    [MaxLength(1000)]
    public string MessageText { get; set; } = null!;
}