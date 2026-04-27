namespace InternSystemProject.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AccountController : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return RedirectToAction("Register", "Auth");
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return RedirectToAction("Login", "Auth");
    }

    [HttpGet]
    [Authorize]
    public IActionResult Logout()
    {
        return RedirectToAction("Logout", "Auth");
    }
}
