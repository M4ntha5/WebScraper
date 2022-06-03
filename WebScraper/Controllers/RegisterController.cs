using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebScraper.Controllers;

public class RegisterController : Controller
{
  public IActionResult Index()
  {
    return View();
  }
  
  public IActionResult Register()
  {
    return Ok();
  }
}