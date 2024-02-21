using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities.User;

public class UserSetting
{
    [Key]     
    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public Active NotificationsNewsletter { get; set; }
    public Active NotificationsFollowers { get; set; }
    public Active NotificationsComments { get; set; }
    public Active NotificationsMessages { get; set; }
}