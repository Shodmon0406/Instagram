using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities;

public class UserSearchHistory
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    [MaxLength(50)]
    public string ApplicationUserSearchId { get; set; } = null!;
    public ApplicationUser ApplicationUserSearch { get; set; } = null!;
    public DateTime SearchDate { get; set; }
}