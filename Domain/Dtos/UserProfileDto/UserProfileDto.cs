using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.UserProfileDto;

public class UserProfileDto
{
    public string? FullName { get; set; }
    public DateTimeOffset Dob { get; set; }
    public string? About { get; set; }
}