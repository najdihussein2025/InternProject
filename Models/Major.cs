public class Major
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;       
    public string Duration { get; set; } = "3 Months";
    public string? Description { get; set; }
    public string ThemeColor { get; set; } = "Cyan";       

    // Navigation
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<FinalProject> FinalProjects { get; set; } = new List<FinalProject>();
    public ICollection<Application> AppliedApplications { get; set; } = new List<Application>();
    public ICollection<Application> AcceptedApplications { get; set; } = new List<Application>();
}