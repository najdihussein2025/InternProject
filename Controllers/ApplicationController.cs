using InternSystemProject.Interfaces.Services;
using InternSystemProject.Dtos.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace InternSystemProject.Controllers
{
    [Authorize(Roles = "Intern")]
    public class ApplicationController : BaseController
    {

        private readonly IApplicationService _appService;
        private readonly IMajorService _majorService;

        public ApplicationController(IApplicationService appService, IMajorService majorService)
        {
            _appService = appService;
            _majorService = majorService;

        }

        new private int GetCurrentUserId()
        {
            var idString = User.FindFirst("UserId")?.Value
                        ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return int.Parse(idString ?? "0");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var id = GetCurrentUserId();
            var existingApp = await _appService.GetApplicationByUserIdAsync(id);

            if (existingApp != null)
            {
                return RedirectToAction(nameof(Status));
            }

            ViewBag.Majors = await _majorService.GetAllMajorsAsync(); // Pass majors to the view for dropdown

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(CreateApplicationDto dto)
        {
            if (!ModelState.IsValid) 
                return View("Index", dto);

            var id = GetCurrentUserId();
            var (success, error) = await _appService.SubmitApplicationAsync(dto, id);

            if (!success)
            {
                ModelState.AddModelError("", error);
                return View("Index", dto);
            }

            return RedirectToAction(nameof(Status));
        }

        [HttpGet]
        public async Task<IActionResult> Status()
        {
            var id = GetCurrentUserId();
            var app = await _appService.GetApplicationByUserIdAsync(id);

            if (app == null) return RedirectToAction(nameof(Index));

            return View(app);
        }
    }
}
