using System.Net;
using Domain.Dtos.UserDto;
using Domain.Dtos.UserSearchHistoryDto;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.SearchService;

public class UserSearchHistoryService(DataContext context) : IUserSearchHistoryService
{
    public async Task<Response<bool>> AddUserSearchHistory(AddUserSearchHistoryDto userSearchHistory, string userId)
    {
        try
        {
            var searchHistory = await context.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userSearchHistory.UserSearchId);
            if (searchHistory == null) return new Response<bool>(HttpStatusCode.NotFound, "Search user not found!");
            var existUser = await context.UserSearchHistories.FirstOrDefaultAsync(x =>
                x.ApplicationUserId == userId && x.ApplicationUserSearchId == userSearchHistory.UserSearchId);
            if (existUser != null)
            {
                existUser.SearchDate = DateTime.UtcNow;
                await context.SaveChangesAsync();
                return new Response<bool>(true);
            }
            var user = new UserSearchHistory()
            {
                ApplicationUserId = userId,
                ApplicationUserSearchId = userSearchHistory.UserSearchId,
                SearchDate = DateTime.UtcNow
            };
            await context.UserSearchHistories.AddAsync(user);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GetUserSearchHistoryDto>>> GetUserSearchHistories(string userId)
    {
        try
        {
            var result = await (from u in context.UserSearchHistories
                    where u.ApplicationUserId == userId
                    orderby u.SearchDate descending 
                    select new GetUserSearchHistoryDto()
                    {
                        Id = u.Id,
                        Users = new GetUserDto()
                        {
                            Id = u.ApplicationUserSearch.Id,
                            UserName = u.ApplicationUserSearch.UserName,
                            Avatar = u.ApplicationUserSearch.UserProfile.Image,
                            FullName = u.ApplicationUserSearch.UserProfile.FullName,
                            SubscribersCount =
                                context.FollowingRelationShips.Count(x => x.FollowingId == u.ApplicationUserSearch.Id)
                        }
                    })
                .AsNoTracking().ToListAsync();
            return new Response<List<GetUserSearchHistoryDto>>(result);
        }
        catch (Exception e)
        {
            return new Response<List<GetUserSearchHistoryDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUserSearchHistory(int id)
    {
        try
        {
            var userSearchHistory = await context.UserSearchHistories.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (userSearchHistory == null)
                return new Response<bool>(HttpStatusCode.NotFound, "ApplicationUser search history not found!");
            context.UserSearchHistories.Remove(userSearchHistory);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUserSearchHistories(string userId)
    {
        try
        {
            var userSearchHistories = await context.UserSearchHistories.Where(x => x.ApplicationUserId == userId).ToListAsync();
            context.UserSearchHistories.RemoveRange(userSearchHistories);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}