namespace InternSystemProject.DTOs.Major;

using System.ComponentModel.DataAnnotations;

public class UpdateMajorDto
{
    [Required(ErrorMessage = "Major name is required")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = "3 Months";
    public string ThemeColor { get; set; } = "Cyan";
    [Range(1, 1000, ErrorMessage = "Max interns must be between 1 and 1000")]
    public int MaxInterns { get; set; } = 30;
}
