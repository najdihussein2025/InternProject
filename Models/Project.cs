public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = "Draft";          

    public int MajorId { get; set; }

    public Major Major { get; set; } = null!;
    public ICollection<StudentProject> StudentProjects { get; set; } = new List<StudentProject>();
}