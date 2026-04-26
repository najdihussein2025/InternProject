using Microsoft.AspNetCore.Mvc;

namespace InternSystemProject.Controllers
{
    public class ApplicationController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
