﻿namespace Domain.Dtos.ViewerDtos;

public class ViewerDto
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Name { get; set; }
    
    public int StoryId { get; set; }
}