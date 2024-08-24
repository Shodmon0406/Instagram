﻿using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities.Post;

public class Image
{
    [Key] public int ImageId { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; } = null!;
    [MaxLength(60)] public string ImageName { get; set; } = null!;
}