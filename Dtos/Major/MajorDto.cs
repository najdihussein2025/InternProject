namespace InternSystemProject.DTOs.Major;

public class MajorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = "3 Months";
    public string ThemeColor { get; set; } = "Cyan";
    public int InternsCount { get; set; }
    public int MaxInterns { get; set; }
}
