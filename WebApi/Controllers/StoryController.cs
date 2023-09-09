﻿using Domain.Dtos.StoryDtos;
using Domain.Entities.Post;
using Domain.Responses;
using Infrastructure.Services.StoryServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    [HttpPost("LikeStory")]
    public async Task<Response<string>> LikeStory(int storyID)
    {
        var userId =User.Claims.FirstOrDefault(e => e.Type == "sid").Value;
        return await _storyService.StoryLike(storyID,userId);
    }
    [HttpGet("GetStoryById")]
    public async Task<Response<GetStoryDto>> GetStoryById(int id)
    {
        var token = User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
        var userName = User.Claims.FirstOrDefault(e => e.Type == "name").Value;
        return await _storyService.GetStoryById(id, token,userName);
    }

    [HttpPost("AddStories")]
    public async Task<Response<GetStoryDto>> AddStories(AddStoryDto add)
    {
        var token = User.Claims.FirstOrDefault(e => e.Type == "sid")!.Value;
        return await _storyService.AddStory(add, token);
    }

    [HttpDelete("DeleteStory")]
    public async Task<Response<bool>> DeleteStory(int id)
    {
        return await _storyService.DeleteStory(id);
    }
}