using System.ComponentModel.DataAnnotations;
using System.Net;
using Domain.Dtos.SearchHistoryDto;
using Domain.Dtos.UserSearchHistoryDto;
using Domain.Filters.UserFilter;
using Domain.Responses;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class UserController(IUserService service) : BaseController
{
    [HttpGet("get-users")]
    public async Task<IActionResult> GetUsers([FromQuery]UserFilter filter)
    {
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sid")!.Value;
        var result = await service.GetUsers(filter, userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("delete-user")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await service.DeleteUser(userId);
        return StatusCode(result.StatusCode, result);
    }
}