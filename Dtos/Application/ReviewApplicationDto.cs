// This is where the admin can either approve or reject an application.

using System.ComponentModel.DataAnnotations;

public class ReviewApplicationDto
{
    [Required]
    public int ApplicationId { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;
    public int? AcceptedMajorId { get; set; }
    public string? RejectionReason { get; set; }
}