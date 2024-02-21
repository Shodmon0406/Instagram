using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ExternalAccountDto;

public class ExternalAccountDto
{
    [Required]
    [MaxLength(50)]
    public string UserId { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string FacebookEmail { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string TwitterUsername { get; set; } = null!;
}