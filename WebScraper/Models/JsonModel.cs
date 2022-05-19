using Newtonsoft.Json;

namespace WebScraper.Models
{
  public class JsonModel
  {
    [JsonProperty("status")]
    public string Status { get; set; }
    [JsonProperty("data")]
    public Tracking Data { get; set; }
  }
}
