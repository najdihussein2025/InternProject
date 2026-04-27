namespace InternSystemProject.Interfaces.Services;

using System.Threading.Tasks;
using InternSystemProject.DTOs.User;
using InternSystemProject.Models;
public interface IUserService
{
    Task<(bool Success, string Error)> RegisterAsync(CreateUserDto dto);
    Task<(bool Success, string Error, User? User)> LoginAsync(LoginDto dto);
    Task<List<UserDto>> GetPendingUsersAsync();
    Task<List<UserDto>> GetAcceptedUsersAsync();
    Task<List<UserDto>> GetAllUsersAsync();
    Task<(bool Success, string Error)> CreateUserAsync(CreateUserDto dto);
    Task<List<UserDto>> GetByNameAsync(string name);
}
