using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper.Services
{
  public class Scraper
  {
    private string TrackingLinkOld = "https://vehicle-status-search.netlify.app/";
    private string ExampleVin = "WBSLX9C58DD159859";
    
    private string TrackingLink = "https://vehicle-status-search.netlify.app/.netlify/functions/fetch?vin=";

    public async Task<Tracking> FetchInfo(string vin)
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

      var tracking = parsedJson.Data;

      tracking.KeyStatus = tracking.KeyStatus == "1" ? "Yes" : "No";
      tracking.TitleStatus = tracking.TitleStatus == "1" ? "Yes" : "No";
       
      return parsedJson.Data;

    }




  }
}
