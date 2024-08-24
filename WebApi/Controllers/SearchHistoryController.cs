using System.ComponentModel.DataAnnotations;
using System.Net;
using Domain.Dtos.SearchHistoryDto;
using Domain.Dtos.UserSearchHistoryDto;
using Domain.Responses;
using Infrastructure.Services.SearchService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class SearchHistoryController(IUserSearchHistoryService userSearchHistoryService) : BaseController
{
    [HttpPost("add-user-search-history")]
    public async Task<IActionResult> AddUserSearchHistory(AddUserSearchHistoryDto userSearchHistory)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
            var result = await userSearchHistoryService.AddUserSearchHistory(userSearchHistory, userId);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("get-user-search-histories")]
    public async Task<IActionResult> GetUserSearchHistories()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await userSearchHistoryService.GetUserSearchHistories(userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("delete-user-search-history")]
    public async Task<IActionResult> DeleteUserSearchHistory([Required]int id)
    {
        if (ModelState.IsValid)
        {
            var result = await userSearchHistoryService.DeleteUserSearchHistory(id);
            return StatusCode(result.StatusCode, result);
        }

        var response = new Response<bool>(HttpStatusCode.BadRequest, ModelStateErrors());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("delete-user-search-histories")]
    public async Task<IActionResult> DeleteUserSearchHistories()
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await userSearchHistoryService.DeleteUserSearchHistories(userId);
        return StatusCode(result.StatusCode, result);
    }
}