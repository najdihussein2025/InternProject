namespace InternSystemProject.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InternSystemProject.Data;
using InternSystemProject.Models;
using InternSystemProject.Interfaces.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }


    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users
            .FirstOrDefaultAsync(u =>
                u.Email.ToLower() == email.Trim().ToLower());
    }


    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<Major?> GetMajorByNameAsync(string majorName)
    {
        return await _db.Majors
            .FirstOrDefaultAsync(m => m.Name.ToLower() == majorName.Trim().ToLower());
    }

    // Get all users with Status == "Pending"
    public async Task<List<User>> GetAllPendingAsync()
    {
        return await _db.Users
            .Where(u => u.Status == "Pending")
            .OrderBy(u => u.JoinDate)
            .ToListAsync();
    }


    public async Task CreateAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(int id, string status)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null)
        {
            return;
        }

        user.Status = status;
        await _db.SaveChangesAsync();
    }
    public async Task<List<User>> GetAllAcceptedAsync()
    {

        return await _db.Users
            .Where(u => u.Status == "Active" && u.Role =="Intern")
            .OrderBy(u => u.JoinDate)
            .ToListAsync();
    }
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _db.Users
            .Where(u => u.Role == "Intern")
            .OrderBy(u => u.JoinDate)
            .ToListAsync();
    }
    public async Task<List<User>> GetByNameAsync(string name)
    {
        return await _db.Users
            .Where(u => u.FullName.ToLower().Contains(name.Trim().ToLower()))
            .OrderBy(u => u.JoinDate)
            .ToListAsync();
    }

    public async Task AssignMajorAsync(int userId, int majorId)
    {
        var user = await _db.Users.FindAsync(userId);
        if (user == null) return;

        user.MajorId = majorId;
        await _db.SaveChangesAsync();
    }
}