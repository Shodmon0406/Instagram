using System.ComponentModel.DataAnnotations;
using Domain.Dtos.UserDto;

namespace Domain.Dtos.ChatDto;

public class GetChatDto : ChatDto
{
    [Required] public GetUserShortInfoDto ReceiveUser { get; set; } = null!;
}