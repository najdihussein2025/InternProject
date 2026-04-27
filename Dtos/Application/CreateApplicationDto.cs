namespace InternSystemProject.Dtos.Application;

public class CreateApplicationDto
{
    public string? University { get; set; }
    public int? GraduationYear { get; set; }
    public string? Skills { get; set; }
    public string? GitHubUrl { get; set; }
    public string? CoverNote { get; set; }
    public int AppliedMajorId { get; set; }
}