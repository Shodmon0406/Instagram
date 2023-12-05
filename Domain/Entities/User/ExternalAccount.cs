using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.User;

public class ExternalAccount
{
    [Key] public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    [MaxLength(45)]
    public string? FacebookEmail { get; set; }
    [MaxLength(45)]
    public string? TwitterUsername { get; set; }
}