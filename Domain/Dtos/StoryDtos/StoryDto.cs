using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Entities.Post;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.StoryDtos;

public class StoryDto
{
   
    public int Id { get; set; }
    [MaxLength(50)]
    public string FileName { get; set; } = null!;
    public int? PostId { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    [MaxLength(50)]
    public string UserId { get; set; } = null!;
}