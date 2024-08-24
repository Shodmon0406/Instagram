using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.FollowingRelationshipDto;

public class FollowingRelationShipDto
{
    [Required]
    [MaxLength(50)]
    public string FollowingId { get; set; } = null!;
}