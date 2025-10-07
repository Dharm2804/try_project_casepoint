using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Services;

namespace project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AuthServices _auth;
    public HomeController(ILogger<HomeController> logger, AuthServices auth)
    {
        _logger = logger;
        _auth = auth;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<JsonResult> Logins([FromForm] Vm_login login)
    {
        if (login == null || string.IsNullOrEmpty(login.EmpMail) || string.IsNullOrEmpty(login.EmpPassword))
            return Json(new { message = "Invalid request", success = false });

        var status = await _auth.Login(login);
        if (status)
        {
            string role = await _auth.GetByEmail(login.EmpMail);
            HttpContext.Session.SetString("Role", role);
            HttpContext.Session.SetString("Mail", login.EmpMail);
            return Json(new { message = "Login successfully", success = true, role = role });
        }

        return Json(new { message = "Invalid Credential", success = false, role = "" });
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
