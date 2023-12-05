using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dtos.UserProfileDto;

public class UpdateUserProfileDto : UserProfileDto
{
    [Required]
    public Gender? Gender { get; set; }
    [Required]
    public new string Dob { get; set; } = null!;
    [Required,DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = null!;
    [Required, DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
}