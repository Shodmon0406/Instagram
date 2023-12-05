using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities.User;

public class UserProfile
{
    [Key, ForeignKey("ApplicationUser")] public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string? FullName { get; set; }
    public string? Image { get; set; }
    public Gender? Gender { get; set; }
    public DateTimeOffset Dob { get; set; }
    public string? About { get; set; }
    public DateTime DateUpdated { get; set; }
}