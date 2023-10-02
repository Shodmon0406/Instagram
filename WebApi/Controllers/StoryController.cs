﻿using Domain.Dtos.StoryDtos;
using Domain.Entities.Post;
using Domain.Responses;
using Infrastructure.Services.StoryServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class StoryController : ControllerBase
{
    private readonly IStoryService storyService;

    public StoryController(IStoryService storyService)
    {
        this.storyService = storyService;
    }
    
    [HttpGet("get-stories")]
    public async Task<IActionResult> GetStories(string userId)
    {
        var userTokenId =User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
        var result = await storyService.GetStories(userId, userTokenId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost("LikeStory")]
    public async Task<Response<string>> LikeStory(int storyID)
    {
        var userId =User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
        return await storyService.StoryLike(storyID,userId);
    }
    [HttpGet("GetStoryById")]
    public async Task<Response<GetStoryDto>> GetStoryById(int id)
    {
        var token = User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
        var userName = User.Claims.FirstOrDefault(e => e.Type == "name")!.Value;
        return await storyService.GetStoryById(id, token,userName);
    }

    [HttpPost("AddStories")]
    public async Task<Response<GetStoryDto>> AddStories(AddStoryDto add)
    {
        var token = User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
        return await storyService.AddStory(add, token);
    }

    [HttpDelete("DeleteStory")]
    public async Task<Response<bool>> DeleteStory(int id)
    {
        return await storyService.DeleteStory(id);
    }
}