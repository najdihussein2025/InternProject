namespace InternSystemProject.Interfaces.Services;

using InternSystemProject.Models;

public interface IJwtService
{
    string GenerateToken(User user);
}
