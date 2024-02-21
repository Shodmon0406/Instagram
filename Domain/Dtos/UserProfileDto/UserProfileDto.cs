using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.UserProfileDto;

public class UserProfileDto
{
    [MaxLength(50)]
    public string? FullName { get; set; }
    public DateTimeOffset Dob { get; set; }
    [MaxLength(300)]
    public string? About { get; set; }
}