using Domain.Entities.User;

namespace Domain.Entities;

public class UserSearchHistory
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string ApplicationUserSearchId { get; set; } = null!;
    public ApplicationUser ApplicationUserSearch { get; set; } = null!;
    public DateTime SearchDate { get; set; }
}