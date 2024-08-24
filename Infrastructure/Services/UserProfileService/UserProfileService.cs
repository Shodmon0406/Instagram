using System.Net;
using Domain.Dtos.UserProfileDto;
using Domain.Entities.User;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserProfileService;

public class UserProfileService(DataContext context,
        UserManager<ApplicationUser> userManager,
        IFileService fileService)
    : IUserProfileService
{
    public async Task<Response<GetUserProfileDto>> GetUserProfileById(string id)
    {
        try
        {
            var userProfile = await (from p in context.UserProfiles
                where p.ApplicationUserId == id
                select new GetUserProfileDto()
                {
                    UserName = p.ApplicationUser.UserName!,
                    Gender = p.Gender.ToString()!,
                    FullName = p.FullName,
                    DateUpdated = p.DateUpdated,
                    Dob = p.Dob,
                    About = p.About,
                    Image = p.Image!,
                    PostCount = p.ApplicationUser.Posts.Count,
                    SubscribersCount = context.FollowingRelationShips.Count(x => x.FollowingId == id),
                    SubscriptionsCount =
                        p.ApplicationUser.FollowingRelationShips
                            .Count // context.FollowingRelationShips.Count(x => x.UserId == id)
                }).AsNoTracking().FirstOrDefaultAsync();

            if (userProfile != null)
            {
                return new Response<GetUserProfileDto>(userProfile);
            }

            return new Response<GetUserProfileDto>(HttpStatusCode.NotFound, "ApplicationUser not found");
        }
        catch (Exception e)
        {
            return new Response<GetUserProfileDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<string>> UpdateUserProfile(UpdateUserProfileDto updateUserProfile, string userId)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);
            var existing = await context.UserProfiles.FindAsync(userId);
            existing!.FullName = updateUserProfile.FullName;
            existing.Gender = updateUserProfile.Gender;
            existing.About = updateUserProfile.About;
            user!.PhoneNumber = updateUserProfile.PhoneNumber;
            user.Email = updateUserProfile.Email;
            existing.Dob = new DateTimeOffset();
            var dob = updateUserProfile.Dob.Split('/');
            existing.Dob = existing.Dob.AddYears(int.Parse(dob[0]) - 1);
            existing.Dob = existing.Dob.AddMonths(int.Parse(dob[1]) - 1);
            existing.Dob = existing.Dob.AddDays(int.Parse(dob[2]) - 1);
            existing.DateUpdated = DateTime.UtcNow;
            await userManager.UpdateAsync(user);
            await context.SaveChangesAsync();
            return new Response<string>("Successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateProfilePhoto(ProfilePhotoDto photo, string userId)
    {
        try
        {
            var user = await context.UserProfiles.FindAsync(userId);
            if (user == null) return new Response<string>(HttpStatusCode.NotFound, "ApplicationUser not found!");
            fileService.DeleteFile(user.Image!);
            var newPhoto = fileService.CreateFile(photo.Photo);
            user.Image = newPhoto.Data;
            await context.SaveChangesAsync();
            return new Response<string>("Successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> DeleteProfilePhoto(string userId)
    {
        try
        {
            var userPhoto = await context.UserProfiles.FindAsync(userId);
            var image = userPhoto!.Image;
            if (image != null) fileService.DeleteFile(image);
            userPhoto.Image = string.Empty;
            await context.SaveChangesAsync();
            return new Response<string>("Successfully");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}