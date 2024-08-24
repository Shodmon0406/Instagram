using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities;

public class SearchHistory
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    [MaxLength(50)]
    public string Text { get; set; } = null!;
    public DateTime SearchDate { get; set; }
}