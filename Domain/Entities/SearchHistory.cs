using Domain.Entities.User;

namespace Domain.Entities;

public class SearchHistory
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime SearchDate { get; set; }
}