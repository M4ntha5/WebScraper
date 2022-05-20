using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebScraper.DTO;

namespace WebScraper.Services
{
  public class Scraper
  {
    private readonly string TrackingLink = "https://vehicle-status-search.netlify.app/.netlify/functions/fetch?vin=";

    private readonly ILogService _logger;

    public Scraper(ILogService logger)
    {
      _logger = logger;
    }

    public async Task<Data> FetchInfo(string vin)
    {
      var website = $"{TrackingLink}{vin}";

      HttpClient http = new HttpClient();
      var response = await http.GetByteArrayAsync(website);
      String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
      source = WebUtility.HtmlDecode(source);
      HtmlDocument result = new HtmlDocument();
      result.LoadHtml(source);

      var jsonResult = result.ParsedText;
      var parsedJson = JsonConvert.DeserializeObject<JsonModel>(jsonResult + "}");

      if (parsedJson.Status != "200" || parsedJson.Data?.Vin == null)
      {
        await _logger.LogErrorAsync("VIN not found");
        return null;
      }

      var tracking = parsedJson.Data;

      return tracking;

    }  
  }
}
