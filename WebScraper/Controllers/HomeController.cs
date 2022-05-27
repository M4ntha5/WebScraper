using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using WebScraper.Models;
using WebScraper.Services;

namespace WebScraper.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly Scraper _scraper;
    private readonly ITrackingService _tracking;
    private readonly TrackingContext _context;

    public HomeController(ILogger<HomeController> logger, Scraper scraper, ITrackingService tracking, TrackingContext context)
    {
      _logger = logger;
      _scraper = scraper;
      _tracking = tracking;
      _context = context;
    }

    public async Task<IActionResult> Index()
    {
      var trackings = await _tracking.GetTrackings();

      return View(trackings);
    }

    public async Task<IActionResult> Search([Required] string vin)
    {
      try
      {
        if (string.IsNullOrEmpty(vin))
          return RedirectToAction(nameof(Index));

        var fetchedData = await _scraper.FetchInfo(vin);
        if(fetchedData == null)
        {
          @TempData["Error"] = "VIN not found";
          return RedirectToAction(nameof(Index));
        }

        //check if exists
        var old = await _context.Trackings
          .FirstOrDefaultAsync(x => x.Vin == vin);

        if (old != null)
          await _tracking.UpdateTracking(fetchedData);
        else
          await _tracking.SaveTracking(fetchedData);

        return RedirectToAction(nameof(Index));
      }
      catch(Exception e)
      {
        return RedirectToAction(nameof(Index));
      }
    }

    public async Task<IActionResult> UpdateAll()
    {
      var allTrackings = await _tracking.GetTrackings();

      foreach(var tracking in allTrackings)
      {
        var fetchedData = await _scraper.FetchInfo(tracking.Vin);

        await _tracking.UpdateTracking(fetchedData);
      }

      return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteSelected(int id)
    {
      await _tracking.DeleteTracking(id);
      return RedirectToAction(nameof(Index));
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
