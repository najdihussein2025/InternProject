using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace InternSystemProject.Controllers
{
    [Authorize(Roles = "Intern")]
    public class ApplicationController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Status()
        {
            return View();
        }
    }
}
