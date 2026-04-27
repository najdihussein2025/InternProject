public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Student"; 
    public string? University { get; set; }
    public string? Bio { get; set; }
    public string? Skills { get; set; }       
    public string? GitHubUrl { get; set; }
    public DateTime JoinDate { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Pending";
    public int Progress { get; set; } = 0;   

    public int? MajorId { get; set; }

    public Major? Major { get; set; }
    public ICollection<StudentTask> StudentTasks { get; set; } = new List<StudentTask>();
    public ICollection<StudentProject> StudentProjects { get; set; } = new List<StudentProject>();
    public ICollection<StudentFinalProject> StudentFinalProjects { get; set; } = new List<StudentFinalProject>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}