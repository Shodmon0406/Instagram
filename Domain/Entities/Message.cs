using Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class Message
{
    public int MessageId { get; set; }
    public int ChatId { get; set; }
    public Chat Chat { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string MessageText { get; set; } = null!;
    public DateTime SendMassageDate { get; set; }
}