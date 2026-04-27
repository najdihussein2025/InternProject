public class StudentFinalProject
{
    public int Id { get; set; }
    public int CompletionPct { get; set; } = 0;           
    public DateTime? Deadline { get; set; }
    public string Status { get; set; } = "InProgress";     
    public string? PresentationUrl { get; set; }


    public int UserId { get; set; }
    public int FinalProjectId { get; set; }

    public User User { get; set; } = null!;
    public FinalProject FinalProject { get; set; } = null!;
}