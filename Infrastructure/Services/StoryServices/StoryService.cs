using System.Net;
using AutoMapper;
using Domain.Dtos.StoryDtos;
using Domain.Dtos.ViewerDtos;
using Domain.Entities.Post;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.StoryServices;

public class StoryService(
    IFileService fileService,
    IMapper mapper,
    DataContext context,
    IWebHostEnvironment hostEnvironment)
    : IStoryService

{
    public async Task<Response<List<GetStoriesDto>>> GetStories(string? userId, string userTokenId)
    {
        try
        {
            var userProfile = context.UserProfiles.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(userId))
                userProfile = userProfile.Where(x => x.ApplicationUserId == userId);
            var stories = await (from pf in userProfile
                select new GetStoriesDto()
                {
                    UserId = pf.ApplicationUserId,
                    UserName = pf.ApplicationUser.UserName,
                    Fullname = pf.FullName,
                    UserPhoto = pf.Image,
                    Stories = (from st in context.Stories
                        where st.ApplicationUserId == pf.ApplicationUserId
                        select new GetStoryDto()
                        {
                            Id = st.Id,
                            FileName = st.FileName,
                            CreateAt = st.CreateAt,
                            UserId = st.ApplicationUserId,
                            PostId = st.PostId,
                            ViewerDto = st.ApplicationUserId == userTokenId
                                ? new ViewerDto()
                                {
                                    ViewCount = st.StoryStat.ViewCount,
                                    ViewLike = st.StoryStat.ViewLike
                                }
                                : null
                        }).AsNoTracking().ToList()
                }).Where(x => x.Stories.Count != 0).AsNoTracking().ToListAsync();
            return new Response<List<GetStoriesDto>>(stories);
        }
        catch (Exception e)
        {
            return new Response<List<GetStoriesDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetStoryDto>> GetStoryById(int id, string userId, string userName)
    {
        try
        {
            var story = await context.Stories.FirstOrDefaultAsync(e => e.Id == id);
            if (story != null)
            {
                var story2 = await (from st in context.Stories
                    join pf in context.UserProfiles on st.ApplicationUserId equals pf.ApplicationUserId
                    where st.Id == id
                    select new GetStoryDto()
                    {
                        Id = st.Id,
                        FileName = st.FileName,
                        CreateAt = st.CreateAt,
                        UserId = st.ApplicationUserId,
                        PostId = st.PostId,
                        ViewerDto = story.ApplicationUserId == userId
                            ? new ViewerDto()
                            {
                                ViewCount = st.StoryStat.ViewCount,
                                ViewLike = st.StoryStat.ViewLike
                            }
                            : null
                    }).AsNoTracking().FirstOrDefaultAsync();
                return new Response<GetStoryDto>(story2);
            }
            else
            {
                return new Response<GetStoryDto>(HttpStatusCode.BadRequest, "Story not found");
            }
        }
        catch (Exception e)
        {
            return new Response<GetStoryDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetStoryDto>> AddStory(AddStoryDto storyDto, string userId)
    {
        try
        {
            var file1 = new Story()
            {
                ApplicationUserId = userId,
                PostId = storyDto.PostId,
            };
            if (file1.PostId == null)
            {
                var fileName = fileService.CreateFile(storyDto.Image).Data;
                file1.FileName = fileName;
            }
            else
            {
                var image = await context.Images.Where(x => x.PostId == storyDto.PostId)
                    .Select(x => x.ImageName)
                    .FirstOrDefaultAsync();
                if (image != null)
                {
                    file1.FileName = image;
                }
                else
                {
                    return new Response<GetStoryDto>(HttpStatusCode.BadRequest, "Post not found");
                }
            }

            await context.Stories.AddAsync(file1);
            await context.SaveChangesAsync();
            var stat = new StoryStat()
            {
                StoryId = file1.Id
            };
            await context.StoryStats.AddAsync(stat);
            await context.SaveChangesAsync();

            var mapped = mapper.Map<GetStoryDto>(file1);
            return new Response<GetStoryDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetStoryDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> StoryLike(int storyId, string userId)
    {
        try
        {
            var story = await context.Stories.FindAsync(storyId);
            if (story == null) return new Response<string>(HttpStatusCode.BadRequest, "Story not found");
            var user = await context.StoryLikes.FirstOrDefaultAsync(e =>
                e.ApplicationUserId == userId && e.StoryId == storyId);
            var stat = await context.StoryStats.FirstAsync(s => s.StoryId == story.Id);
            if (user == null)
            {
                stat.ViewLike++;
                var storyLike = new StoryLike()
                {
                    StoryId = storyId,
                    ApplicationUserId = userId,
                };
                await context.StoryLikes.AddAsync(storyLike);
                await context.SaveChangesAsync();
                return new Response<string>("Liked");
            }
            else
            {
                stat.ViewLike--;
                context.StoryLikes.Remove(user);
                await context.SaveChangesAsync();
                return new Response<string>("Disliked");
            }
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteStory(int id)
    {
        try
        {
            var story = context.Stories.FirstOrDefault(e => e.Id == id);
            if (story == null) return new Response<bool>(false);
            var path = Path.Combine(hostEnvironment.WebRootPath, "images", story.FileName);
            File.Delete(path);
            context.Stories.Remove(story);
            await context.SaveChangesAsync();
            return new Response<bool>(true);

        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}