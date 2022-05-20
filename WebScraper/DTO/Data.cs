using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WebScraper.DTO
{
  public class Data
  {
    [JsonProperty("vin")]
    public string Vin { get; set; }

    [JsonProperty("year")]
    public string Year { get; set; }

    [JsonProperty("model")]
    public string Model { get; set; }

    [JsonProperty("keysStatus")]
    public string KeyStatus { get; set; }

    [JsonProperty("deliveredToLoadingPlace")]
    public DateTime? DeliveredToLoadingPlace { get; set; }

    [JsonProperty("titleStatus")]
    public string TitleStatus { get; set; }

    [JsonProperty("containerNumber")]
    public string ContainerNumber { get; set; }

    [JsonProperty("expectedArrivalDate")]
    public DateTime? ExpectedArrivalDate { get; set; }

    [JsonProperty("images")]
    public List<string> Images { get; set; }
  }
}
