﻿using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities.Post;

public class Story
{
    [Key]
    public int Id { get; set; }

    [MaxLength(60)] public string FileName { get; set; } = null!;
    public int? PostId { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; }  = null!;

    public List<StoryView> StoryViews { get; set; } = null!;
    public Post Post { get; set; } = null!;
    public List<StoryLike> StoryLikes { get; set; } = null!;
    public StoryStat StoryStat{ get; set; } = null!;
    public List<StoryUser> StoryUsers { get; set; } = null!;
}