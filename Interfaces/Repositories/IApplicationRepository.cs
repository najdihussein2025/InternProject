namespace InternSystemProject.Interfaces.Repositories;

using System.Threading.Tasks;
public interface IApplicationRepository
{
    Task CreateAsync(Application application); // Create a new application
    Task<Application?> GetByIdAsync(int id); // Get an application by ID in order for the admin to review it
    Task<Application?> GetByUserIdAsync(int userId); // Important for checking if an intern has already applied
    Task<List<Application>> GetAllPendingAsync(); // Get all pending applications for the admin to review
    Task UpdateStatusAsync(Application application); // Where the admin can update the status of an application

}