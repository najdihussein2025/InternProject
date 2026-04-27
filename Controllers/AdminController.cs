namespace InternSystemProject.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InternSystemProject.Helpers;
using InternSystemProject.Interfaces.Services;

public class AdminController : BaseController
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService, SessionHelper session)
        : base(session)
    {
        _userService = userService;
    }

    // ── DASHBOARD ────────────────────────────────

    [HttpGet]
    public IActionResult Dashboard()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Dashboard";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult AcceptedInterns()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Accepted Interns";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Archived()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Archived";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Users()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Users";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Majors()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Majors";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult MajorDetails(int id)
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Major Details";
        ViewBag.AdminName = _session.GetCurrentUserName();
        ViewBag.MajorId = id;
        return View();
    }

    [HttpGet]
    public IActionResult Tasks()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Tasks";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult FinalProjects()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Final Projects";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Reports()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Reports";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Settings()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Settings";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult ProgressTracking()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Progress Tracking";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpGet]
    public IActionResult Notifications()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Notifications";
        ViewBag.AdminName = _session.GetCurrentUserName();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult MarkAllRead()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        TempData["Success"] = "All notifications marked as read.";
        return RedirectToAction("Notifications");
    }

    [HttpGet]
    public IActionResult UserDetails(int id)
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "User Details";
        ViewBag.AdminName = _session.GetCurrentUserName();
        ViewBag.UserId = id;
        return View();
    }

    [HttpGet]
    public IActionResult MajorInterns(int id)
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Major Interns";
        ViewBag.AdminName = _session.GetCurrentUserName();
        ViewBag.MajorId = id;
        return View();
    }

    // ── APPLICATIONS (approve/reject) ────────────

    [HttpGet]
    public async Task<IActionResult> Applications()
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

        ViewBag.PageTitle = "Applications";
        ViewBag.AdminName = _session.GetCurrentUserName();

        var pending = await _userService.GetPendingUsersAsync();
        return View(pending);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApproveUser(int id)
    {
        var auth = RequireAdmin();
        if (auth != null) return auth;

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
        var auth = RequireAdmin();
        if (auth != null) return auth;

        var (success, error) = await _userService.RejectUserAsync(id);

        if (success)
            TempData["Success"] = "Intern rejected.";
        else
            TempData["Error"] = error;

        return RedirectToAction("Applications");
    }
}
