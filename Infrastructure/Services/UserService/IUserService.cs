using Domain.Dtos.SearchHistoryDto;
using Domain.Dtos.UserDto;
using Domain.Dtos.UserSearchHistoryDto;
using Domain.Filters;
using Domain.Filters.UserFilter;
using Domain.Responses;

namespace Infrastructure.Services.UserService;

public interface IUserService
{
    Task<PagedResponse<List<GetUserDto>>> GetUsers(UserFilter filter, string userId);
    
    Task<Response<bool>> DeleteUser(string id);
}