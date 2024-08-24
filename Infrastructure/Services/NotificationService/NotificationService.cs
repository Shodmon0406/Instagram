using System.Net;
using Domain.Dtos.UserDto;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.NotificationService;

public class NotificationService(DataContext context) : INotificationService
{
    public async Task<Response<List<GetUserShortInfoDto>>> Users(string userId)
    {
        try
        {
            var users = await (from pl in context.PostUserLikes
                where pl.PostLike.Post.ApplicationUserId == userId
                select new GetUserShortInfoDto()
                {
                    UserId = pl.ApplicationUserId,
                    UserName = pl.ApplicationUser.UserName,
                    Fullname = pl.ApplicationUser.UserProfile.FullName,
                    UserPhoto = pl.ApplicationUser.UserProfile.Image,
                    Subscriptions = context.FollowingRelationShips.AsNoTracking()
                        .Any(x => x.ApplicationUserId == userId && x.FollowingId == pl.ApplicationUserId)
                }).AsNoTracking().ToListAsync();
            return new Response<List<GetUserShortInfoDto>>(users);
        }
        catch (Exception e)
        {
            return new Response<List<GetUserShortInfoDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}