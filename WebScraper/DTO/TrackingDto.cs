using System;
using System.Collections.Generic;

namespace WebScraper.Models
{
  public class TrackingDto
  {
    public int Id { get; set; }
    public string Vin { get; set; }

    public int Year { get; set; }

    public string Model { get; set; }

    public string Keys { get; set; }

    public DateTime? DeliveredToLoadingPlace { get; set; }

    public string Title { get; set; }

    public string ContainerNumber { get; set; }

    public DateTime? ExpectedArrivalDate { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<string> Images { get; set; }
  }
}
