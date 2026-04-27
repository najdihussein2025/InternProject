<<<<<<< HEAD
namespace InternSystemProject.Interfaces.Repositories;

using InternSystemProject.DTOs.Major;
using InternSystemProject.Models;

public interface IMajorRepository
{
    Task<Major?> GetByNameAsync(string name);
    Task<Major?> GetByIdAsync(int id);
    Task<List<Major>> GetAllAsync();
    Task CreateAsync(Major major);
    Task UpdateAsync(Major major);
    Task DeleteAsync(int id);
}