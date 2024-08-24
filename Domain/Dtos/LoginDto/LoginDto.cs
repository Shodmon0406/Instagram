using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.LoginDto;

public class LoginDto
{
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Password { get; set; } = null!;
}