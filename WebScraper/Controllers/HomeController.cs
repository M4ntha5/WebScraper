using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebScraper.Models;
using WebScraper.Services;

namespace WebScraper.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly Scraper _scraper;

    public HomeController(ILogger<HomeController> logger, Scraper scraper)
    {
      _logger = logger;
      _scraper = scraper;
    }

    public async Task<IActionResult> Index()
    {
      var vin = "WBSLX9C58DD159859";
      var data = await _scraper.FetchInfo(vin);

      return View(data);
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
}
