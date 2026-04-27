// This is where the admin can view the details of an application

public class ApplicationDetailsDto
{
    public int Id { get; set; }
    public string University { get; set; } = string.Empty;
    public string CoverNote { get; set; } = string.Empty;
    public string Skills { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}