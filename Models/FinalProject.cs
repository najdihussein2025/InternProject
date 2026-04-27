public class FinalProject
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Timeline { get; set; } = "2 Weeks";
    public string? TeamMode { get; set; }                  
    public string Status { get; set; } = "Draft";        

    public int MajorId { get; set; }

    public Major Major { get; set; } = null!;
    public ICollection<StudentFinalProject> StudentFinalProjects { get; set; } = new List<StudentFinalProject>();
}