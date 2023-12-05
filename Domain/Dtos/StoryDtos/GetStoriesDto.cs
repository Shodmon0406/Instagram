using Domain.Dtos.UserDto;

namespace Domain.Dtos.StoryDtos;

public class GetStoriesDto
{
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string UserPhoto { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public List<GetStoryDto> Stories { get; set; } = null!;
}