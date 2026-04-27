using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternSystemProject.Interfaces.Repositories;

using InternSystemProject.Models;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task<Major?> GetMajorByNameAsync(string majorName);
    Task<List<User>> GetAllPendingAsync();
    Task<List<User>> GetAllAcceptedAsync();
    Task UpdateStatusAsync(int id, string status);
    Task<List<User>> GetAllUsersAsync();
    Task CreateAsync(User user);
<<<<<<< HEAD
    Task<List<User>> GetByNameAsync(string name);
=======
    Task AssignMajorAsync(int userId, int majorId);
>>>>>>> 9269a7e (Added Application Logic for Interns)
}