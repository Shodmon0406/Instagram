using Domain.Dtos.UserDto;
using Domain.Responses;

namespace Infrastructure.Services.NotificationService;

public interface INotificationService
{
    Task<Response<List<GetUserShortInfoDto>>> Users(string userId);
}