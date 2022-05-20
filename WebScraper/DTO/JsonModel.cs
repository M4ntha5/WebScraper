using Newtonsoft.Json;

namespace WebScraper.DTO
{
  public class JsonModel
  {
    [JsonProperty("status")]
    public string Status { get; set; }
    [JsonProperty("data")]
    public Data Data { get; set; }
  }
}
