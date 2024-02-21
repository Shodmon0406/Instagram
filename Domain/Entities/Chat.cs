using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities;
public class Chat
{
    public int ChatId { get; set; }
    [MaxLength(50)]
    public string SendUserId { get; set; } = null!;
    public ApplicationUser SendUser { get; set; } = null!;
    [MaxLength(50)]
    public string ReceiveUserId { get; set; } = null!;
    public ApplicationUser ReceiveUser { get; set; } = null!;
    public List<Message> Messages { get; set; } = null!;
}