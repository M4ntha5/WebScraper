using System;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper.Services
{
  public class LogService : ILogService
  {
    private readonly TrackingContext _context;

    public LogService(TrackingContext context)
    {
      _context = context;
    }

    public Task LogErrorAsync(string message)
    {
      _context.Logs.Add(new Log
      {
        Level = 1,
        Message = message,
        Date = DateTime.Now
      });
      return _context.SaveChangesAsync();
    }

    public Task LogInfoAsync(string message)
    {
      _context.Logs.Add(new Log
      {
        Level = 0,
        Message = message,
        Date = DateTime.Now
      });
      return _context.SaveChangesAsync();
    }
  }
}
