namespace InternSystemProject.DTOs.User;

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;
    public int Progress { get; set; }
    
    public string Email { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Joined { get; set; }
}
