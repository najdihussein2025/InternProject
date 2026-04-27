namespace InternSystemProject.Services;

using System.Threading.Tasks;
using InternSystemProject.DTOs.Major;
using InternSystemProject.Interfaces.Repositories;
using InternSystemProject.Interfaces.Services;
using InternSystemProject.Models;

public class MajorService : IMajorService
{
    private readonly IMajorRepository _majorRepo;

    public MajorService(IMajorRepository majorRepo){
        _majorRepo = majorRepo;
    }

    public async Task<(bool Success, string Error)> CreateMajorAsync(CreateMajorDto dto){
        var existing = await _majorRepo.GetByNameAsync(dto.Name);
        if (existing != null)
            return (false, "A major with this name already exists.");

        var major = new Major{
            Name = dto.Name.Trim(),
            Description = dto.Description?.Trim(),
            Duration = string.IsNullOrWhiteSpace(dto.Duration) ? "3 Months" : dto.Duration.Trim(),
            ThemeColor = string.IsNullOrWhiteSpace(dto.ThemeColor) ? "Cyan" : dto.ThemeColor.Trim(),
            MaxInterns = dto.MaxInterns
        };
        await _majorRepo.CreateAsync(major);
        return (true, string.Empty);
    }

    public async Task<List<MajorDto>> GetAllMajorsAsync()
    {
        var majors = await _majorRepo.GetAllAsync();
        return majors.Select(m => new MajorDto
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description ?? string.Empty,
            Duration = m.Duration,
            ThemeColor = m.ThemeColor,
            InternsCount = m.Users.Count,
            MaxInterns = m.MaxInterns
        }).ToList();
    }

    public async Task<MajorDto> GetMajorByIdAsync(int id)
    {
        var major = await _majorRepo.GetByIdAsync(id);
        if (major == null)
        {
            return new MajorDto();
        }

        return new MajorDto
        {
            Id = major.Id,
            Name = major.Name,
            Description = major.Description ?? string.Empty,
            Duration = major.Duration,
            ThemeColor = major.ThemeColor,
            InternsCount = major.Users.Count,
            MaxInterns = major.MaxInterns
        };
    }

    public async Task<(bool Success, string Error)> UpdateMajorAsync(int id, UpdateMajorDto dto)
    {
        var major = await _majorRepo.GetByIdAsync(id);
        if (major == null)
            return (false, "Major not found.");

        var duplicate = await _majorRepo.GetByNameAsync(dto.Name);
        if (duplicate != null && duplicate.Id != id)
            return (false, "A major with this name already exists.");

        major.Name = dto.Name.Trim();
        major.Description = dto.Description?.Trim();
        major.Duration = string.IsNullOrWhiteSpace(dto.Duration) ? "3 Months" : dto.Duration.Trim();
        major.ThemeColor = string.IsNullOrWhiteSpace(dto.ThemeColor) ? "Cyan" : dto.ThemeColor.Trim();
        major.MaxInterns = dto.MaxInterns;

        await _majorRepo.UpdateAsync(major);
        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> DeleteMajorAsync(int id)
    {
        var major = await _majorRepo.GetByIdAsync(id);
        if (major == null)
            return (false, "Major not found.");

        await _majorRepo.DeleteAsync(id);
        return (true, string.Empty);
    }
}
