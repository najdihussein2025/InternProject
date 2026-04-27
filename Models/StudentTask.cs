public class StudentTask
{
    public int Id { get; set; }
    public DateTime? Deadline { get; set; }
    public string Status { get; set; } = "Pending";     
    public string? SubmissionUrl { get; set; }
    public string? Feedback { get; set; }

    public int UserId { get; set; }
    public int TaskId { get; set; }

    public User User { get; set; } = null!;
    public InternTask Task { get; set; } = null!;
}