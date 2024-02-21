using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.UserSearchHistoryDto;

public class AddUserSearchHistoryDto : UserSearchHistoryDto
{
    [Required]
    [MaxLength(50)]
    public string UserSearchId { get; set; } = null!;
}