using Microsoft.EntityFrameworkCore;
using InternSystemProject.Models;

namespace InternSystemProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    public DbSet<User> Users { get; set; }
    public DbSet<Major> Majors { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<FinalProject> FinalProjects { get; set; }
    public DbSet<StudentTask> StudentTasks { get; set; }
    public DbSet<StudentProject> StudentProjects { get; set; }
    public DbSet<StudentFinalProject> StudentFinalProjects { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Application> Applications { get; set; }
    

    }
}
