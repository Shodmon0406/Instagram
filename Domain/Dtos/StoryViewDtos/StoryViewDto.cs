using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.StoryViewDtos;

public class StoryViewDto
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string ViewUserId { get; set; } = null!;
    public int StoryId { get; set; }
}