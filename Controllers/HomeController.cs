using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcStarter.Models;
using Starter_Proxy;

namespace AspnetCoreMvcStarter.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly ServiceProxy _serviceProxy;

  public HomeController(ILogger<HomeController> logger, ServiceProxy proxy)
  {
    _logger = logger;
    _serviceProxy = proxy;
  }

  public IActionResult Index() => View();

  public IActionResult Login()
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

  public IActionResult NotFound() => View();
}
