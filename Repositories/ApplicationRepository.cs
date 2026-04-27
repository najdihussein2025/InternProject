namespace InternSystemProject.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InternSystemProject.Data;
using InternSystemProject.Models;
using InternSystemProject.Interfaces.Repositories;

public class  ApplicationRepository : IApplicationRepository
{

    private readonly AppDbContext _db;

    public ApplicationRepository(AppDbContext db)
    {
        _db = db;
    }


    // Create a new application
    public async Task CreateAsync(Application application)
    {
        await _db.Applications.AddAsync(application);
        await _db.SaveChangesAsync();
    }

    // Get a single application by its ID
    public async Task<Application?> GetByIdAsync(int id)
    {
        return await _db.Applications.FindAsync(id);
    }


    // Get an application by the ID of the user to check if the user has already applied
    public async Task<Application?> GetByUserIdAsync(int userId)
    {
        return await _db.Applications
            .Include(a => a.AppliedMajor)
            .Where(a => a.UserId == userId)
            .OrderByDescending(a => a.SubmittedAt)
            .FirstOrDefaultAsync();
    }

    // Get all pending applications for the admin to review
    public async Task<List<Application>> GetAllPendingAsync()
    {
        return await _db.Applications.Where(a => a.Status == "Pending").OrderBy(a => a.SubmittedAt).ToListAsync();
    }

    // Update the status of an application
    public async Task UpdateStatusAsync(Application application)
    {
        _db.Applications.Update(application);

        await _db.SaveChangesAsync();
    }

}

