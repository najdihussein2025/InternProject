using Microsoft.AspNetCore.Mvc;

namespace InternSystemProject.Controllers
{
    public class StudentController : Controller
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
            SetPage("Tasks");
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
        public IActionResult Courses()
        {
            SetPage("Courses");
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
            ViewBag.StudentName = "Sara Ahmed";
            ViewBag.UnreadCount = 2;
        }
    }
}
