using System.Threading.Tasks;

namespace WebScraper.Services
{
  public interface ILogService
  {
    Task LogErrorAsync(string message);
    Task LogInfoAsync(string message);
  }
}
