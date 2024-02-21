using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.CategoryDto;

public class CategoryDto
{
    [MaxLength(50)] public string CategoryName { get; set; } = null!;
}