﻿using System.Net;
using Domain.Dtos.UserProfileDto;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Services.PostService;
using Infrastructure.Services.UserProfileService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class UserProfileController(IUserProfileService userProfileService,
        IPostService postService)
    : BaseController
{
    [HttpGet("get-user-profile-by-id")]
    public async Task<IActionResult> GetUserProfileById(string id)
    {
        var result = await userProfileService.GetUserProfileById(id);
        return StatusCode(result.StatusCode, result);
    }

   
    
    [HttpPut("update-user-profile")]
    public async Task<IActionResult> UpdateUserProfile([FromBody]UpdateUserProfileDto userProfile)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
            var result = await userProfileService.UpdateUserProfile(userProfile,userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<UserProfileDto>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("update-profile-photo")]
    public async Task<IActionResult> UpdateProfilePhoto([FromForm]ProfilePhotoDto photo)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
            var result = await userProfileService.UpdateProfilePhoto(photo, userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<string>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-profile-photo")]
    public async Task<IActionResult> DeleteProfilePhoto()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await userProfileService.DeleteProfilePhoto(userId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("get-post-favorites")]
    public async Task<IActionResult> GetPostFavorites([FromQuery] PaginationFilter filter)
    {
        var userId = User.Claims.FirstOrDefault(u => u.Type == "sid")!.Value;
        var result = await postService.GetPostFavorites(filter, userId);
        return StatusCode(result.StatusCode, result);
    }
    
    // statistic profile
    /*[HttpGet("CounterProfile")]
    public async Task<Response<GetStatistic>> GetCountPost()
    {
        var userId = ApplicationUser.Claims.FirstOrDefault(c => c.Type == "sid")!.Value;
        var post = await statisticFollowAndPostService.GetUserPost(userId);
        var following = await statisticFollowAndPostService.GetFollowing(userId);
        var follower = await statisticFollowAndPostService.GetFollowers(userId);
        var test = new GetStatistic()
        {
            Post = post.Data,
            Follower = follower.Data,
            Following = following.Data
        };
        return new Response<GetStatistic>(test);
    }*/
}