namespace InternSystemProject.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InternSystemProject.DTOs.User;
using InternSystemProject.Helpers;
using InternSystemProject.Interfaces.Services;

public class AuthController : BaseController
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService, SessionHelper session)
        : base(session)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (_session.IsLoggedIn())
            return RedirectToDashboard(_session.GetCurrentUserRole());

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var (success, error) = await _userService.RegisterAsync(dto);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, error);
            return View(dto);
        }

        TempData["Success"] = "Account created! Please wait for admin approval.";
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (_session.IsLoggedIn())
            return RedirectToDashboard(_session.GetCurrentUserRole());

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var (success, error, role) = await _userService.LoginAsync(dto);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, error);
            return View(dto);
        }

        return RedirectToDashboard(role);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        _session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult LogoutPost()
    {
        _session.Clear();
        return RedirectToAction("Index", "Home");
    }

    private IActionResult RedirectToDashboard(string? role)
    {
        return role switch
        {
            "Admin" => RedirectToAction("Dashboard", "Admin"),
            "Intern" => RedirectToAction("Dashboard", "Student"),
            _ => RedirectToAction("Index", "Home")
        };
    }
}
