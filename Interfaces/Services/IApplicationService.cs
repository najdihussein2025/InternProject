namespace InternSystemProject.Interfaces.Services;

using InternSystemProject.Dtos.Application;
using System.Threading.Tasks;
public interface  IApplicationService
{

    Task<(bool Success, string Error)> SubmitApplicationAsync(CreateApplicationDto dto, int userId); // Where the intern apply for an internship
    Task<ApplicationDetailsDto?> GetApplicationByUserIdAsync(int userId); // Where the intern can either view their application status or submit a new application if they haven't applied yet
    Task<List<ApplicationListDto>> GetAllPendingApplicationsAsync(); // Where the admin can view all pending applications to review
    Task<(bool Success, string Error)> ApproveApplicationAsync(int applicationId, int acceptedMajorId); // Where the admin approves the application and assigns the accepted major
    Task<(bool Success, string Error)> RejectApplicationAsync(int applicationId, string reason); // Where the admin rejects the application and provides a reason for rejection


}