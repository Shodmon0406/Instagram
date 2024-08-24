using Infrastructure.Services.NotificationService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class NotificationController(INotificationService service) : BaseController
{
    [HttpGet("get-notification-about-likes")]
    public async Task<IActionResult> GetUserForLike()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await service.Users(userId);
        return StatusCode(result.StatusCode, result);
    }
}