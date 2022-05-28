using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebScraper.DTO;

namespace WebScraper.Services
{
  public class Scraper
  {
    private const string TrackingLink = "https://vehicle-status-search.netlify.app/.netlify/functions/fetch?vin=";

    private readonly ILogService _logger;

    public Scraper(ILogService logger)
    {
      _logger = logger;
    }

    public async Task<Data> FetchInfo(string vin)
    {
      var website = $"{TrackingLink}{vin}";

      var http = new HttpClient();
      var response = await http.GetByteArrayAsync(website);
      var source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
      source = WebUtility.HtmlDecode(source);
      var result = new HtmlDocument();
      result.LoadHtml(source);

      var jsonResult = result.ParsedText;
      var parsedJson = JsonConvert.DeserializeObject<JsonModel>(jsonResult + "}");

      if (parsedJson?.Status != "200" || string.IsNullOrEmpty(parsedJson.Data?.Vin))
      {
        await _logger.LogErrorAsync("VIN not found");
        return null;
      }

      var tracking = parsedJson.Data;

      return tracking;

    }  
  }
}
