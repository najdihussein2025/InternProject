namespace InternSystemProject.Controllers;

using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

public class BaseController : Controller
{
    protected int? GetCurrentUserId()
    {
        var id = User.FindFirst("UserId")?.Value
            ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return int.TryParse(id, out var parsed) ? parsed : null;
    }

    protected string? GetCurrentUserRole()
    {
        return User.FindFirst("Role")?.Value
            ?? User.FindFirst(ClaimTypes.Role)?.Value;
    }

    protected string? GetCurrentUserName()
    {
        return User.FindFirst("FullName")?.Value
            ?? User.FindFirst(ClaimTypes.Name)?.Value;
    }
}
