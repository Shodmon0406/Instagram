using Domain.Dtos.UserProfileDto;
using Domain.Responses;

namespace Infrastructure.Services.UserProfileService;

public interface IUserProfileService
{
    Task<Response<GetUserProfileDto>> GetUserProfileById(string userId);
    Task<Response<string>> UpdateUserProfile(UpdateUserProfileDto addUserProfile,string userId);
    Task<Response<string>> UpdateProfilePhoto(ProfilePhotoDto photo,string userId);
    Task<Response<string>> DeleteProfilePhoto(string userId);
}