﻿using System.ComponentModel.DataAnnotations;
using Domain.Entities.User;

namespace Domain.Entities.Post;

public class PostFavoriteUser
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public int PostFavoriteId { get; set; }
    public PostFavorite PostFavorite { get; set; } = null!;
}