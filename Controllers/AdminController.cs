namespace InternSystemProject.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternSystemProject.Interfaces.Services;

[Authorize(Roles = "Admin")]
public class AdminController : BaseController
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
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
    public IActionResult AcceptedInterns()
    {
        ViewBag.PageTitle = "Accepted Interns";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Archived()
    {
        ViewBag.PageTitle = "Archived";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Users()
    {
        ViewBag.PageTitle = "Users";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Majors()
    {
        ViewBag.PageTitle = "Majors";
        ViewBag.AdminName = GetCurrentUserName();
        return View();
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

        var pending = await _userService.GetPendingUsersAsync();
        return View(pending);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApproveUser(int id)
    {
        var (success, error) = await _userService.ApproveUserAsync(id);

        if (success)
            TempData["Success"] = "Intern approved successfully.";
        else
            TempData["Error"] = error;

        return RedirectToAction("Applications");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RejectUser(int id)
    {
        var (success, error) = await _userService.RejectUserAsync(id);

        if (success)
            TempData["Success"] = "Intern rejected.";
        else
            TempData["Error"] = error;

        return RedirectToAction("Applications");
    }
}
