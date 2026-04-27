public class StudentProject
{
    public int Id { get; set; }
    public string Status { get; set; } = "Pending";       
    public string? SubmissionUrl { get; set; }

    public int UserId { get; set; }
    public int ProjectId { get; set; }

    public User User { get; set; } = null!;
    public Project Project { get; set; } = null!;
}