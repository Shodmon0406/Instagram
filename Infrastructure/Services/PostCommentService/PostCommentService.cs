using System.Net;
using AutoMapper;
using Domain.Dtos.PostCommentDto;
using Domain.Entities.Post;
using Domain.Filters.PostCommentFilter;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.PostCommentService;

public class PostCommentService(DataContext context, IMapper mapper) : IPostCommentService
{
    public async Task<PagedResponse<List<GetPostCommentDto>>> GetPostComments(PostCommentFilter filter)
    {
        try
        {
            var comments = context.PostComments.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Comment))
                comments = comments.Where(c => c.Comment.ToLower().Contains(filter.Comment.ToLower()));
            var response = await comments
                .Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var totalRecord = comments.Count();
            var mapped = mapper.Map<List<GetPostCommentDto>>(response);
            return new PagedResponse<List<GetPostCommentDto>>(mapped, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPostCommentDto>>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<GetPostCommentDto>> GetPostCommentById(int id)
    {
        try
        {
            var comment = await context.PostComments.FindAsync(id);
            var mapped = mapper.Map<GetPostCommentDto>(comment);
            return new Response<GetPostCommentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetPostCommentDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<GetPostCommentDto>> AddPostComment(AddPostCommentDto addPostComment)
    {
        try
        {

            var post = await context.Posts.FindAsync(addPostComment.PostId);
            if (post == null)
                return new Response<GetPostCommentDto>(HttpStatusCode.BadRequest, "Post not found");
            var comment = mapper.Map<PostComment>(addPostComment);
            await context.PostComments.AddAsync(comment);
            await context.SaveChangesAsync();
            var postCommentLike = new PostCommentLike() { PostCommentId = comment.PostCommentId };
            await context.PostCommentLikes.AddAsync(postCommentLike);
            await context.SaveChangesAsync();
            var mapped = mapper.Map<GetPostCommentDto>(comment);

            return new Response<GetPostCommentDto>(mapped);
        }
        catch (Exception e)
        {
            return new Response<GetPostCommentDto>(HttpStatusCode.BadRequest, e.Message);
        }
    }

    public async Task<Response<bool>> LikeCommentPost(LikeCommentPostDto commentLike)
    {
        var countLike = await context.PostCommentLikes.FindAsync(commentLike.PostId);

        var like = await context.ListOfUserCommentLikes.FirstOrDefaultAsync(e => e.PostCommentLikeId == commentLike.PostId && e.ApplicationUserId == commentLike.UserId);
        if (like != null)
        {
             context.ListOfUserCommentLikes.Remove(like);
            countLike!.LikeCount--;
            await context.PostCommentLikes.AddAsync(countLike);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        await context.ListOfUserCommentLikes.AddAsync(like!);
        countLike!.LikeCount++;
        await context.PostCommentLikes.AddAsync(countLike);
        await context.SaveChangesAsync();
        return new Response<bool>(true);
    }

    public async Task<Response<bool>> DeletePostComment(int id)
    {
        try
        {
            var comment = await context.PostComments.FindAsync(id);
            if (comment == null) return new Response<bool>(HttpStatusCode.BadRequest, "Comment not found");
            context.PostComments.Remove(comment);
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.BadRequest, e.Message);
        }
    }
}