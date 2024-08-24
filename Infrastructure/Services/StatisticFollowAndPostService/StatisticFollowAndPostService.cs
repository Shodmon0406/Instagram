using System.Net;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StatisticFollowAndPostService;

public class StatisticFollowAndPostService(DataContext context)
    : IStatisticFollowAndPostService
{
    public async Task<Response<int>> GetUserPost(string userId)
    {
        try
        {
            var existing = context.Users.FirstOrDefault(x => x.Id == userId);

            if (existing == null) return new Response<int>(HttpStatusCode.NotFound, "not found");
            var post = await context.Posts.Where(x => x.ApplicationUser.Id == userId).CountAsync();
            return new Response<int>(post);
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> GetFollowing(string userId)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                var userFollow = await context.FollowingRelationShips.Where(x => x.ApplicationUserId == userId)
                    .CountAsync();
                return new Response<int>(userFollow);
            }

            return new Response<int>(HttpStatusCode.BadRequest, "not found");
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<int>> GetFollowers(string userId)
    {
        try
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user != null)
            {
                var userFollow = await context.FollowingRelationShips.Where(x => x.FollowingId == userId).CountAsync();
                return new Response<int>(userFollow);
            }

            return new Response<int>(HttpStatusCode.BadRequest, "not found");
        }
        catch (Exception e)
        {
            return new Response<int>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}