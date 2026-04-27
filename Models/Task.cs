public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Phase { get; set; } = "Month 1";       
    public string DueWeek { get; set; } = "Week 1";       
    public string Status { get; set; } = "Draft";          

    public int MajorId { get; set; }

    public Major Major { get; set; } = null!;
    public ICollection<StudentTask> StudentTasks { get; set; } = new List<StudentTask>();
}