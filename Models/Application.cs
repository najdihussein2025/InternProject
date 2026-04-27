using System.ComponentModel.DataAnnotations.Schema;

public class Application
{
    public int Id { get; set; }
    public string? University { get; set; }
    public int? GraduationYear { get; set; }
    public string? Skills { get; set; }
    public string? GitHubUrl { get; set; }
    public string? CoverNote { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Pending";       
    public string? RejectionReason { get; set; }

    public int AppliedMajorId { get; set; }
    public int? AcceptedMajorId { get; set; }
    public int UserId { get; set; }

    [ForeignKey(nameof(AppliedMajorId))]
    [InverseProperty(nameof(Major.AppliedApplications))]
    public Major AppliedMajor { get; set; } = null!;

    [ForeignKey(nameof(AcceptedMajorId))]
    [InverseProperty(nameof(Major.AcceptedApplications))]
    public Major? AcceptedMajor { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}