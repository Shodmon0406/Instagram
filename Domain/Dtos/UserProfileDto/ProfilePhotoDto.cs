using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.UserProfileDto;

public class ProfilePhotoDto
{
    [Required]
    public IFormFile Photo { get; set; } = null!;
}