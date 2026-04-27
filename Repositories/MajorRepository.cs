namespace InternSystemProject.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InternSystemProject.Data;
using InternSystemProject.Models;
using InternSystemProject.Interfaces.Repositories;
public class MajorRepository : IMajorRepository{
    private readonly AppDbContext _db;
    public MajorRepository(AppDbContext db){
        _db = db;
    }
    public async Task<Major?> GetByNameAsync(string name){
        return await _db.Majors.FirstOrDefaultAsync(m => m.Name.ToLower() == name.Trim().ToLower());
    }
    public async Task<Major?> GetByIdAsync(int id){
        return await _db.Majors
            .Include(m => m.Users)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    public async Task<List<Major>> GetAllAsync(){
        return await _db.Majors
            .Include(m => m.Users)
            .ToListAsync();
    }
    public async Task CreateAsync(Major major){
        await _db.Majors.AddAsync(major);
        await _db.SaveChangesAsync();
    }
    public async Task UpdateAsync(Major major){
        _db.Majors.Update(major);
        await _db.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id){
        var major = await _db.Majors.FindAsync(id);
        if (major == null)
        {
            return;
        }

        _db.Majors.Remove(major);
        await _db.SaveChangesAsync();
    }
}