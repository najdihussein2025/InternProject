namespace InternSystemProject.Interfaces.Services;

using System.Threading.Tasks;
using InternSystemProject.DTOs.Major;
using InternSystemProject.Models;
public interface IMajorService
{
    Task<(bool Success, string Error)> CreateMajorAsync(CreateMajorDto dto);
    Task<List<MajorDto>> GetAllMajorsAsync();
    Task<MajorDto> GetMajorByIdAsync(int id);
    Task<(bool Success, string Error)> UpdateMajorAsync(int id, UpdateMajorDto dto);
    Task<(bool Success, string Error)> DeleteMajorAsync(int id);

    Task<List<Major>> GetAllForDropdownAsync();
}