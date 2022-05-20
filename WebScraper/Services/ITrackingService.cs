using System.Collections.Generic;
using System.Threading.Tasks;
using WebScraper.DTO;
using WebScraper.Models;

namespace WebScraper.Services
{
  public interface ITrackingService
  {
    Task<List<TrackingDto>> GetTrackings();
    Task SaveTracking(Data dto);
    Task UpdateTracking(Data fetchedData);
  }
}
