using Domain.Dtos.UserDto;
using Domain.Dtos.ViewerDtos;

namespace Domain.Dtos.StoryDtos;

public class GetStoryDto : StoryDto
{
    public ViewerDto? ViewerDto { get; set; }
}