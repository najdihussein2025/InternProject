using Microsoft.AspNetCore.Mvc;

namespace InternSystemProject.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Login")]
        public IActionResult LoginPost()
        {
            return RedirectToAction("Dashboard", "Admin");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Signup")]
        public IActionResult SignupPost()
        {
            TempData["SuccessMessage"] = "Account created successfully. Please sign in.";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Logout")]
        public IActionResult LogoutPost()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
