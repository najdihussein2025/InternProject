namespace InternSystemProject.Controllers;

using InternSystemProject.DTOs.User;
using InternSystemProject.Interfaces.Services;
using InternSystemProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : BaseController
{
    private readonly IUserService _userService;

    private readonly IMajorService _majorService;
    private readonly IApplicationService _appService;

    public AdminController(IUserService userService, IMajorService majorService, IApplicationService appService)
    {
        _userService = userService;
        _majorService = majorService;
        _appService = appService;

    }

    // ── DASHBOARD ────────────────────────────────

    [HttpGet]
    public IActionResult Dashboard()
    {
        ViewBag.PageTitle = "Dashboard";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> AcceptedInterns()
    {
        ViewBag.PageTitle = "Accepted Interns";
        ViewBag.AdminName = GetCurrentUserName();
        var acceptedUsers = await _userService.GetAcceptedUsersAsync();
        return View(acceptedUsers);
    }

    [HttpGet]
    public async Task<IActionResult> Users(string? name)
    {
        ViewBag.PageTitle = "Users";
        ViewBag.AdminName = GetCurrentUserName();
        ViewBag.SearchTerm = name ?? string.Empty;

        var users = string.IsNullOrWhiteSpace(name)
            ? await _userService.GetAllUsersAsync()
            : await _userService.GetByNameAsync(name);

        return View(users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(CreateUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please provide valid user information.";
            return RedirectToAction("Users");
        }

        var (success, error) = await _userService.CreateUserAsync(dto);
        if (!success)
        {
            TempData["Error"] = error;
            return RedirectToAction("Users");
        }

        TempData["Success"] = "User created successfully.";
        return RedirectToAction("Users");
    }

    [HttpGet]
    public async Task<IActionResult> Majors()
    {
        ViewBag.PageTitle = "Majors";
        ViewBag.AdminName = GetCurrentUserName();
        var majors = await _majorService.GetAllMajorsAsync();
        return View(majors);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateMajor(InternSystemProject.DTOs.Major.CreateMajorDto dto)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Please provide valid major information.";
            return RedirectToAction("Majors");
        }

        var (success, error) = await _majorService.CreateMajorAsync(dto);
        if (!success)
        {
            TempData["Error"] = error;
            return RedirectToAction("Majors");
        }

        TempData["Success"] = "Major created successfully.";
        return RedirectToAction("Majors");
    }

    [HttpGet]
    public IActionResult MajorDetails(int id)
    {
        ViewBag.PageTitle = "Major Details";
        ViewBag.AdminName = GetCurrentUserName();
        ViewBag.MajorId = id;
        return View();
    }

    [HttpGet]
    public IActionResult Tasks()
    {
        ViewBag.PageTitle = "Tasks";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult FinalProjects()
    {
        ViewBag.PageTitle = "Final Projects";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Reports()
    {
        ViewBag.PageTitle = "Reports";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Settings()
    {
        ViewBag.PageTitle = "Settings";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult ProgressTracking()
    {
        ViewBag.PageTitle = "Progress Tracking";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Notifications()
    {
        ViewBag.PageTitle = "Notifications";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult MarkAllRead()
    {
        TempData["Success"] = "All notifications marked as read.";
        return RedirectToAction("Notifications");
    }

    [HttpGet]
    public IActionResult UserDetails(int id)
    {
        ViewBag.PageTitle = "User Details";
        ViewBag.AdminName = GetCurrentUserName();
        ViewBag.UserId = id;
        return View();
    }

    [HttpGet]
    public IActionResult MajorInterns(int id)
    {
        ViewBag.PageTitle = "Major Interns";
        ViewBag.AdminName = GetCurrentUserName();
        ViewBag.MajorId = id;
        return View();
    }

    // ── APPLICATIONS (approve/reject) ────────────

    [HttpGet]
    public async Task<IActionResult> Applications()
    {
        ViewBag.PageTitle = "Applications";
        ViewBag.AdminName = GetCurrentUserName();

        var pending = await _appService.GetAllPendingApplicationsAsync(); // Changed to get pending applications
        return View(pending);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApproveUser(int id, int majorId)
    {
        var (success, error) = await _appService.ApproveApplicationAsync(id, majorId); // Changed to approve function of the application service

        if (success)
            TempData["Success"] = "Intern approved successfully.";
        else
            TempData["Error"] = error;

        return RedirectToAction("Applications");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RejectUser(int id, string reason)
    {
        var (success, error) = await _appService.RejectApplicationAsync(id, reason); // Changed to reject function of the application service

        if (success)
            TempData["Success"] = "Intern rejected.";
        else
            TempData["Error"] = error;

        return RedirectToAction("Applications");
    }
}
