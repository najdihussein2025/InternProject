namespace InternSystemProject.Interfaces.Services;

using System.Threading.Tasks;
using InternSystemProject.DTOs.User;
using InternSystemProject.Models;
public interface IUserService
{
    Task<(bool Success, string Error)> RegisterAsync(CreateUserDto dto);
    Task<(bool Success, string Error, User? User)> LoginAsync(LoginDto dto);
    Task<List<UserDto>> GetPendingUsersAsync();
    Task<(bool Success, string Error)> ApproveUserAsync(int id);
    Task<(bool Success, string Error)> RejectUserAsync(int id);
}
