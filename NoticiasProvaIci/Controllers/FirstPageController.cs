using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NoticiasProvaIci.Models;

namespace NoticiasProvaIci.Controllers;

public class FirstPageController : Controller
{
    public IActionResult Initial()
    {
        return View();
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