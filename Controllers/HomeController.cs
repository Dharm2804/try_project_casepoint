using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Services;

namespace project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AuthServices _service;

    public HomeController(ILogger<HomeController> logger, AuthServices service)
    {
        _logger = logger;
        _service = service;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<JsonResult> Register(EmployeeModel emp)
    {
        if (!ModelState.IsValid)
        {
            // Collect model validation errors
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();

            return Json(new
            {
                success = false,
                message = "Validation failed",
                errors
            });
        }

       

        // Check if email already exists
        if (await _service.IsEmailExistsAsync(emp.EmpMail))
        {
            return Json(new
            {
                success = false,
                message = "Email already exists. Please try another."
            });
        }

        bool isRegistered = await _service.RegisterAsync(emp);

        if (isRegistered)
        {
            return Json(new
            {
                success = true,
                message = "Registration successful!"
            });
        }
        else
        {
            return Json(new
            {
                success = false,
                message = "Registration failed due to server error."
            });
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
