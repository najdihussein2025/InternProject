using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternSystemProject.Interfaces.Repositories;

using InternSystemProject.Models;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);
    Task<List<User>> GetAllPendingAsync();
    Task CreateAsync(User user);
    Task UpdateStatusAsync(int id, string status);
}