using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dtos.RegisterDto;

public class RegisterDto
{
    [Required]
    [MaxLength(20)]
    public string UserName { get; set; } = null!;
    [Required]
    [MaxLength(20)]
    public string FullName { get; set; } = null!;
    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = null!;
    [DataType(DataType.Password)]
    [Required]
    [MaxLength(50), MinLength(4)]
    public string Password { get; set; } = null!;
    [Compare("Password")][DataType(DataType.Password)]
    [Required]
    [MaxLength(50),  MinLength(4)]
    public string ConfirmPassword { get; set; } = null!;
}