namespace InternSystemProject.Controllers;

using Microsoft.AspNetCore.Mvc;
using InternSystemProject.Helpers;

public class BaseController : Controller
{
    protected readonly SessionHelper _session;

    public BaseController(SessionHelper session)
    {
        _session = session;
    }

    protected IActionResult? RequireAdmin()
    {
        var role = _session.GetCurrentUserRole();

        if (role == null)
            return RedirectToAction("Login", "Auth");

        if (role != "Admin")
            return RedirectToAction("Index", "Home");

        return null; 
    }

    protected IActionResult? RequireIntern()
    {
        var role = _session.GetCurrentUserRole();

        if (role == null)
            return RedirectToAction("Login", "Auth");

        if (role != "Intern")
            return RedirectToAction("Index", "Home");

        return null; 
    }
}
