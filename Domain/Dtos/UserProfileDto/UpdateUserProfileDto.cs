using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dtos.UserProfileDto;

public class UpdateUserProfileDto : UserProfileDto
{
    [Required]
    public Gender Gender { get; set; }
    [Required, MaxLength(50)]
    public new string Dob { get; set; } = null!;
    [Required,DataType(DataType.PhoneNumber), MaxLength(50)]
    public string PhoneNumber { get; set; } = null!;
    [Required, DataType(DataType.EmailAddress), MaxLength(50)]
    public string Email { get; set; } = null!;
}