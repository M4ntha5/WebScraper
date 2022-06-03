using Microsoft.AspNetCore.Mvc;

namespace WebScraper.Controllers;

public class LoginController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
  
  public IActionResult Login()
  {
    return Ok();
  }
}