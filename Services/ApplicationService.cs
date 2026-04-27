namespace InternSystemProject.Services;

using InternSystemProject.Dtos.Application;
using InternSystemProject.Interfaces.Repositories;
using InternSystemProject.Interfaces.Services;
using InternSystemProject.Models;

public class ApplicationService : IApplicationService
{

    private readonly IApplicationRepository _appRepo;
    private readonly IUserRepository _userRepo;

    public ApplicationService(IApplicationRepository appRepo, IUserRepository userRepo)
    {
        _appRepo = appRepo;
        _userRepo = userRepo;
    }

    // Submit the application that the user created
    public async Task<(bool Success, string Error)> SubmitApplicationAsync(CreateApplicationDto dto, int userId)
    {
        var existingApp = await _appRepo.GetByUserIdAsync(userId);

        if (existingApp != null && existingApp.Status == "Pending")
            return (false, "You already have a pending application.");

        if (existingApp != null && existingApp.Status == "Approved")
            return (false, "You have already been accepted.");

        var newApp = new Application
        {
            UserId = userId,
            University = dto.University,
            GraduationYear = dto.GraduationYear,
            Skills = dto.Skills,
            GitHubUrl = dto.GitHubUrl,
            CoverNote = dto.CoverNote,
            AppliedMajorId = dto.AppliedMajorId,
            Status = "Pending",
            SubmittedAt = DateTime.Now
        };

        await _appRepo.CreateAsync(newApp);

        return (true, string.Empty);
    }

    // Get an application by the user ID

    public async Task<ApplicationDetailsDto?> GetApplicationByUserIdAsync(int userId)
    {
        var userApp = await _appRepo.GetByUserIdAsync(userId);

        if(userApp == null)
        {
            return null;
        }

        return new ApplicationDetailsDto
        {
            Id = userApp.Id,
            University = userApp.University ?? "N/A",
            CoverNote = userApp.CoverNote ?? "N/A",
            Skills = userApp.Skills ?? "N/A",
            Status = userApp.Status,
        };
    }

    public async Task<List<ApplicationListDto>> GetAllPendingApplicationsAsync()
    {
        var applications = await _appRepo.GetAllPendingAsync();

        return applications.Select(a => new ApplicationListDto
        {
            Id = a.Id,
            University = a.University ?? "N/A",
            Status = a.Status,
            SubmittedAt = a.SubmittedAt
        }).ToList();
    }

    public async Task<(bool Success, string Error)> ApproveApplicationAsync(int applicationId, int acceptedMajorId)
    {
        var app = await _appRepo.GetByIdAsync(applicationId);
        if (app == null)
        {
            return (false, "Application not found.");
        }

        app.Status = "Approved";
        app.AcceptedMajorId = acceptedMajorId;
        app.RejectionReason = null;
        await _appRepo.UpdateStatusAsync(app);

        await _userRepo.AssignMajorAsync(app.UserId, acceptedMajorId);
        await _userRepo.UpdateStatusAsync(app.UserId, "Active");

        return (true, string.Empty);
    }

    public async Task<(bool Success, string Error)> RejectApplicationAsync(int applicationId, string reason)
    {
        var app = await _appRepo.GetByIdAsync(applicationId);
        if (app == null)
        {
            return (false, "Application not found.");
        }

        app.Status = "Rejected";
        app.RejectionReason = reason;

        await _appRepo.UpdateStatusAsync(app);
        return (true, string.Empty);
    }
}