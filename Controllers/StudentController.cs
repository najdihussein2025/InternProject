namespace InternSystemProject.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Intern")]
public class StudentController : BaseController
{
    [HttpGet]
    public IActionResult Dashboard()
    {
        SetPage("Dashboard");
        return View();
    }

    [HttpGet]
    public IActionResult Tasks()
    {
        SetPage("My Tasks");
        return View();
    }

    [HttpGet]
    public IActionResult Projects()
    {
        SetPage("Projects");
        return View();
    }

    [HttpGet]
    public IActionResult Progress()
    {
        SetPage("Progress");
        return View();
    }

    [HttpGet]
    public IActionResult FinalProject()
    {
        SetPage("Final Project");
        return View();
    }

    [HttpGet]
    public IActionResult Profile()
    {
        SetPage("Profile");
        return View();
    }

    [HttpGet]
    public IActionResult Settings()
    {
        SetPage("Settings");
        return View();
    }

    private void SetPage(string title)
    {
        ViewBag.PageTitle = title;
        ViewBag.StudentName = GetCurrentUserName();
        ViewBag.UnreadCount = 2;
    }
}
