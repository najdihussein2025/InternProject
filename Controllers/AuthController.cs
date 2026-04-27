namespace InternSystemProject.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InternSystemProject.DTOs.User;
using InternSystemProject.Interfaces.Services;

public class AuthController : BaseController
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService, IJwtService jwtService, IConfiguration configuration)
    {
        _userService = userService;
        _jwtService = jwtService;
        _configuration = configuration;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToDashboard(GetCurrentUserRole(), "Active");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
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
    [AllowAnonymous]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToDashboard(GetCurrentUserRole(), "Active");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var (success, error, user) = await _userService.LoginAsync(dto);

        if (!success)
        {
            ModelState.AddModelError(string.Empty, error);
            return View(dto);
        }

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Unable to complete login.");
            return View(dto);
        }

        if (user.Status == "Rejected")
        {
            ModelState.AddModelError(string.Empty, "Your application was rejected.");
            return View(dto);
        }

        var token = _jwtService.GenerateToken(user);
        var expireMinutes = int.TryParse(_configuration.GetSection("Jwt")["ExpireMinutes"], out var minutes) ? minutes : 120;

        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddMinutes(expireMinutes),
            IsEssential = true
        });

        return RedirectToDashboard(user.Role, user.Status);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult LogoutPost()
    {
        Response.Cookies.Delete("jwt");
        return RedirectToAction("Index", "Home");
    }

    private IActionResult RedirectToDashboard(string? role, string? status)
    {
        if (status == "Pending")
            return RedirectToAction("Status", "Application");

        if (status == "Rejected")
            return RedirectToAction("Status", "Application");

        return role switch
        {
            "Admin" => RedirectToAction("Dashboard", "Admin"),
            "Intern" => RedirectToAction("Dashboard", "Student"),
            _ => RedirectToAction("Index", "Home")
        };
    }
}
