using Microsoft.AspNetCore.Mvc;

namespace InternSystemProject.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Dashboard()
        {
            SetSharedViewBags("Dashboard");
            return View();
        }

        [HttpGet]
        public IActionResult Users()
        {
            SetSharedViewBags("Users");
            return View();
        }

        [HttpGet]
        public IActionResult Applications()
        {
            SetSharedViewBags("Applications");
            return View();
        }

        [HttpGet]
        public IActionResult AcceptedInterns()
        {
            SetSharedViewBags("Accepted Interns");
            return View();
        }

        [HttpGet]
        public IActionResult Archived()
        {
            SetSharedViewBags("Archived");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AcceptApplication(int id, string major)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(major))
            {
                TempData["Error"] = "Unable to accept application. Please select a valid major.";
                return RedirectToAction(nameof(Applications));
            }

            TempData["Success"] = $"Application #{id} accepted and assigned to {major}.";
            return RedirectToAction(nameof(Applications));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RejectApplication(int id, string reason)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(reason))
            {
                TempData["Error"] = "Please provide a rejection reason before submitting.";
                return RedirectToAction(nameof(Applications));
            }

            TempData["Success"] = $"Application #{id} was rejected successfully.";
            return RedirectToAction(nameof(Applications));
        }

        [HttpGet]
        public IActionResult Majors()
        {
            SetSharedViewBags("Majors");
            return View();
        }

        [HttpGet]
        public IActionResult MajorDetails(int id = 1)
        {
            SetSharedViewBags("Major Details");
            ViewBag.MajorId = id;
            return View();
        }

        [HttpGet]
        public IActionResult MajorInterns(int id = 1)
        {
            SetSharedViewBags("Major Interns");
            ViewBag.MajorId = id;
            return View();
        }

        [HttpGet]
        public IActionResult Tasks()
        {
            SetSharedViewBags("Tasks");
            return View();
        }

        [HttpGet]
        public IActionResult FinalProjects()
        {
            SetSharedViewBags("Final Projects");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddMajor()
        {
            TempData["Success"] = "Major created successfully.";
            return RedirectToAction(nameof(Majors));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMajor(int id)
        {
            if (id <= 0)
            {
                TempData["Error"] = "Invalid major selection.";
                return RedirectToAction(nameof(Majors));
            }

            TempData["Success"] = "Major deleted successfully.";
            return RedirectToAction(nameof(Majors));
        }

        [HttpGet]
        public IActionResult ProgressTracking()
        {
            SetSharedViewBags("Progress Tracking");
            return View();
        }

        [HttpGet]
        public IActionResult Reports()
        {
            SetSharedViewBags("Reports");
            return View();
        }

        [HttpGet]
        public IActionResult Notifications()
        {
            SetSharedViewBags("Notifications");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MarkAllRead()
        {
            TempData["Success"] = "All notifications marked as read.";
            return RedirectToAction(nameof(Notifications));
        }

        [HttpGet]
        public IActionResult Settings()
        {
            SetSharedViewBags("Settings");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveProfile()
        {
            TempData["Success"] = "Profile updated successfully.";
            return RedirectToAction(nameof(Settings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword()
        {
            TempData["Success"] = "Password updated successfully.";
            return RedirectToAction(nameof(Settings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCompany()
        {
            TempData["Success"] = "Company information saved.";
            return RedirectToAction(nameof(Settings));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNotificationPrefs()
        {
            TempData["Success"] = "Notification preferences updated.";
            return RedirectToAction(nameof(Settings));
        }

        [HttpGet]
        public IActionResult UserDetail(int id)
        {
            return RedirectToAction(nameof(UserDetails), new { id });
        }

        [HttpGet]
        public IActionResult UserDetails(int id = 1)
        {
            SetSharedViewBags("User Details");
            ViewBag.UserId = id;
            return View();
        }

        private void SetSharedViewBags(string pageTitle)
        {
            ViewBag.PageTitle = pageTitle;
            ViewBag.AdminName = "Maya Hassan";
            ViewBag.PendingCount = 4;
            ViewBag.UnreadCount = 3;
        }
    }
}
