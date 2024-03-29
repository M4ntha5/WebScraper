﻿using System;
using System.Collections.Generic;

namespace WebScraper.Models
{
  public class Tracking
  {
    public int Id { get; set; }
    public string Vin { get; set; }

    public int Year { get; set; }

    public string Model { get; set; }

    public bool Keys { get; set; }

    public DateTime? DeliveredToLoadingPlace { get; set; }

    public bool Title { get; set; }

    public string ContainerNumber { get; set; }

    public DateTime? ExpectedArrivalDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    public virtual List<TrackingImage> TrackingImages { get; set; }
  }
}
