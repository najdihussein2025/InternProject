namespace InternSystemProject.Controllers;

using Microsoft.AspNetCore.Mvc;
using InternSystemProject.Helpers;

public class StudentController : BaseController
{
    public StudentController(SessionHelper session) : base(session) { }

    [HttpGet]
    public IActionResult Dashboard()
    {
        var auth = RequireIntern();
        if (auth != null) return auth;
        SetPage("Dashboard");
        return View();
    }

    [HttpGet]
    public IActionResult Tasks()
    {
        var auth = RequireIntern();
        if (auth != null) return auth;
        SetPage("My Tasks");
        return View();
    }

    [HttpGet]
    public IActionResult Projects()
    {
        var auth = RequireIntern();
        if (auth != null) return auth;
        SetPage("Projects");
        return View();
    }

    [HttpGet]
    public IActionResult Progress()
    {
        var auth = RequireIntern();
        if (auth != null) return auth;
        SetPage("Progress");
        return View();
    }

    [HttpGet]
    public IActionResult FinalProject()
    {
        var auth = RequireIntern();
        if (auth != null) return auth;
        SetPage("Final Project");
        return View();
    }

    [HttpGet]
    public IActionResult Profile()
    {
        var auth = RequireIntern();
        if (auth != null) return auth;
        SetPage("Profile");
        return View();
    }

    [HttpGet]
    public IActionResult Settings()
    {
        var auth = RequireIntern();
        if (auth != null) return auth;
        SetPage("Settings");
        return View();
    }

    private void SetPage(string title)
    {
        ViewBag.PageTitle = title;
        ViewBag.StudentName = _session.GetCurrentUserName();
        ViewBag.UnreadCount = 2;
    }
}
