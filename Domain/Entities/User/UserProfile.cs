﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Post;
using Domain.Enums;

namespace Domain.Entities.User;

public class UserProfile
{
    [Key,ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? LocationId { get; set; } 
    public Location Location { get; set; }
    public string? Image { get; set; }
    public Gender? Gender { get; set; }
    public DateTime DOB { get; set; } = DateTime.UtcNow;
    public string? Occupation { get; set; }
    public string? About { get; set; }
    public DateTime DateUpdated { get; set; }
}