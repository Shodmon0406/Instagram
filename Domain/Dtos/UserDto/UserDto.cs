using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dtos.UserDto;

public class UserDto
{
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = null!;
}