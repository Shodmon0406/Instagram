using System.Net;
using Domain.Dtos.UserDto;
using Domain.Filters.UserFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace Infrastructure.Services.UserService;

public class UserService(DataContext context)
    : IUserService
{
    public async Task<PagedResponse<List<GetUserDto>>> GetUsers(UserFilter filter, string userId)
    {
        try
        {
            var users = context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(filter.UserName))
                users = users.Where(u => u.UserName!.ToLower().Contains(filter.UserName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Email))
                users = users.Where(u => u.Email!.ToLower().Contains(filter.Email.ToLower()));
            var result = await (from u in users
                    select new GetUserDto()
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Avatar = u.UserProfile.Image,
                        FullName = u.UserProfile.FullName,
                        SubscribersCount = context.FollowingRelationShips.Count(x => x.FollowingId == u.Id),
                        Subscriptions =
                            context.FollowingRelationShips.AsNoTracking().Any(x => x.ApplicationUserId == userId && x.FollowingId == u.Id)
                    })
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .AsNoTracking().ToListAsync();
            var totalRecord = users.Count();

            return new PagedResponse<List<GetUserDto>>(result, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetUserDto>>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUser(string id)
    {
        try
        {
            var user = await context.Users.FindAsync(id);
            if (user == null) return new Response<bool>(HttpStatusCode.BadRequest, "ApplicationUser not found");
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }
}