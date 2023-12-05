using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.User;

[Index("ApplicationUserId", "FollowingId", IsUnique = true)]
public class FollowingRelationShip
{
    [Key]
    public int FollowingRelationShipId { get; set; }

    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string FollowingId { get; set; } = null!;
    public ApplicationUser Following { get; set; } = null!;
    public DateTime DateFollowed { get; set; }
}