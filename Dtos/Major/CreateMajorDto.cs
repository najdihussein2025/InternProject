namespace InternSystemProject.DTOs.Major;

using System.ComponentModel.DataAnnotations;

public class CreateMajorDto
{
    [Required(ErrorMessage = "Major name is required")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = "3 Months";
    public string ThemeColor { get; set; } = "Cyan";
    [Range(1, 30, ErrorMessage = "Max interns must be between 1 and 30")]
    public int MaxInterns { get; set; } = 30;

}