using Domain.Dtos.UserSearchHistoryDto;
using Domain.Responses;

namespace Infrastructure.Services.SearchService;

public interface IUserSearchHistoryService
{
    Task<Response<bool>> AddUserSearchHistory(AddUserSearchHistoryDto userSearchHistory, string userId);
    Task<Response<List<GetUserSearchHistoryDto>>> GetUserSearchHistories(string userId);
    Task<Response<bool>> DeleteUserSearchHistory(int id);
    Task<Response<bool>> DeleteUserSearchHistories(string userId);
}