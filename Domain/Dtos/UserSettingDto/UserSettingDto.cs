using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Dtos.UserSettingDto;

public class UserSettingDto
{
    [MaxLength(50)] public string UserId { get; set; } = null!;
    public Active NotificationsNewsletter { get; set; }
    public Active NotificationsFollowers { get; set; }
    public Active NotificationsComments { get; set; }
    public Active NotificationsMessages { get; set; }
}