using Microsoft.AspNetCore.Mvc;

namespace WebScraper.Controllers;

public class ComingSoonController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
}