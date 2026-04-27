// This is where the admin can view the list of applications

namespace InternSystemProject.Dtos.Application;

public class ApplicationListDto
{
    public int Id { get; set; }
    public string University { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
}