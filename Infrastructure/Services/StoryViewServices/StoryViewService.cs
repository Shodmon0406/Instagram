using System.Net;
using AutoMapper;
using Domain.Dtos.StoryViewDtos;
using Domain.Entities.Post;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StoryViewServices;

public class StoryViewService(DataContext context, IMapper mapper) : IStoryViewService
{
    public async Task<Response<GetStoryViewDto>> AddStoryView(AddStoryViewDto model, string userId)
    {
        try
        {
            var story = context.Stories.FirstOrDefault(e => e.Id == model.StoryId);
            if (story != null)
            {
                var storyView = new StoryView()
                {
                    ApplicationUserId = userId,
                    StoryId = model.StoryId,
                };
                var existView =
                    await context.StoryUsers.FirstOrDefaultAsync(e =>
                        e.StoryId == model.StoryId && e.ApplicationUserId == userId);
                var stat = context.StoryStats.FirstOrDefault(e => e.StoryId == story.Id);
                if (existView == null)
                {
                    stat.ViewCount++;
                    var view = new StoryUser()
                    {
                      StoryId = model.StoryId,
                      ApplicationUserId = userId
                    };
                    await context.StoryViews.AddAsync(storyView);
                    context.StoryStats.Update(stat);
                    await context.StoryUsers.AddAsync(view);
                    await context.SaveChangesAsync();
                    var mapped = mapper.Map<GetStoryViewDto>(storyView);
                    return new Response<GetStoryViewDto>(mapped);
                }
                else
                {
                    var mapped = mapper.Map<GetStoryViewDto>(storyView);
                    return new Response<GetStoryViewDto>(mapped);
                }
            }
            else
            {
                return new Response<GetStoryViewDto>(HttpStatusCode.BadRequest, "Story not found");
            }
        }
        catch (Exception e)
        {
            return new Response<GetStoryViewDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}