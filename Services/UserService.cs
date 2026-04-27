namespace InternSystemProject.Services;

using System.Threading.Tasks;
using InternSystemProject.DTOs.User;
using InternSystemProject.Helpers;
using InternSystemProject.Interfaces.Repositories;
using InternSystemProject.Interfaces.Services;
using InternSystemProject.Models;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;

    public UserService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<(bool Success, string Error)> RegisterAsync(CreateUserDto dto)
    {

        var existing = await _userRepo.GetByEmailAsync(dto.Email);
        if (existing != null)
            return (false, "An account with this email already exists.");

        var user = new User
        {
            FullName = $"{dto.FirstName.Trim()} {dto.LastName.Trim()}".Trim(),
            Email = dto.Email.Trim().ToLower(),
            PasswordHash = PasswordHasher.Hash(dto.Password),
            Role = "Intern",
            Status = "Pending",
            JoinDate = DateTime.Now
        };


        await _userRepo.CreateAsync(user);

        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error, User? User)> LoginAsync(LoginDto dto)
    {

        var user = await _userRepo.GetByEmailAsync(dto.Email);

        if (user == null)
            return (false, "Invalid credentials.", null);

        if (!PasswordHasher.Verify(dto.Password, user.PasswordHash))
            return (false, "Invalid credentials.", null);

        return (true, string.Empty, user);
    }

    // Return all pending users as DTOs
    public async Task<List<UserDto>> GetPendingUsersAsync()
    {
        var users = await _userRepo.GetAllPendingAsync();

        return users.Select(u => new UserDto
        {
            Id = u.Id,
            FullName = u.FullName,
            Email = u.Email,
            Status = u.Status,
            CreatedAt = u.JoinDate
        }).ToList();
    }

    // Approve: set Status to Active
    public async Task<(bool Success, string Error)> ApproveUserAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user == null)
            return (false, "User not found.");

        if (user.Status != "Pending")
            return (false, "User is not in pending state.");

        await _userRepo.UpdateStatusAsync(id, "Active");

        return (true, string.Empty);
    }

    // Reject: set Status to Rejected
    public async Task<(bool Success, string Error)> RejectUserAsync(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user == null)
            return (false, "User not found.");

        if (user.Status != "Pending")
            return (false, "User is not in pending state.");

        await _userRepo.UpdateStatusAsync(id, "Rejected");

        return (true, string.Empty);
    }
}