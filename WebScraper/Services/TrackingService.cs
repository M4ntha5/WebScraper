﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScraper.DTO;
using WebScraper.Models;

namespace WebScraper.Services
{
  public class TrackingService : ITrackingService
  {
    private readonly TrackingContext _context;
    private readonly ILogService _logger;

    public TrackingService(TrackingContext context, ILogService logger)
    {
      _context = context;
      _logger = logger;
    }

    public async Task DeleteTracking(int carId)
    {
      var tracking = await _context.Trackings
        .Include(x => x.TrackingImages)
        .FirstOrDefaultAsync(x => x.Id == carId);

      if (tracking != null)
      {
        _context.Trackings.Remove(tracking);
        await _context.SaveChangesAsync();
      }
    }

    public async Task SaveTracking(Data dto)
    {
      try
      {
        if (dto?.Vin == null)
        {
          await _logger.LogErrorAsync("Error saving tracking, data is null");
          return;
        }
        
        _context.Trackings.Add(new Tracking
        {
          Vin = dto.Vin,
          Year = int.Parse(dto.Year),
          Model = dto.Model,
          Keys = dto.KeyStatus == "1",
          DeliveredToLoadingPlace = dto.DeliveredToLoadingPlace,
          Title = dto.TitleStatus == "1",
          ContainerNumber = dto.ContainerNumber,
          ExpectedArrivalDate = dto.ExpectedArrivalDate,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();
      }
      catch(Exception e)
      {
        await _logger.LogErrorAsync($"Error saving tracking. {e.Message} {e.InnerException?.Message}");
      }
    }

    public async Task<List<TrackingDto>> GetTrackings()
    {
      try
      {
        var res =  await _context.Trackings
          .Include(x => x.TrackingImages)
          .Select(x => new TrackingDto
          {
            Id = x.Id,
            Vin = x.Vin,
            Year = x.Year,
            Model = x.Model,
            Keys = x.Keys ? "Yes" : "No",
            DeliveredToLoadingPlace = x.DeliveredToLoadingPlace,
            Title = x.Title ? "Yes" : "No",
            ContainerNumber = x.ContainerNumber,
            ExpectedArrivalDate = x.ExpectedArrivalDate,
            UpdatedAt = x.UpdatedAt,
            Images = x.TrackingImages
            .Select(i => i.ImageLink)
            .ToList()
          })
          .OrderByDescending(x => x.UpdatedAt)
          .ToListAsync();
        return res;
      }
      catch (Exception e)
      {
        await _logger.LogErrorAsync($"Error getting trackings. {e.Message} {e.InnerException?.Message}");
        return new List<TrackingDto>();
      }
    }

    public async Task UpdateTracking(Data fetchedData)
    {
      var old = await _context.Trackings
        .Include(x => x.TrackingImages)
        .FirstOrDefaultAsync(x => x.Vin == fetchedData.Vin);
      if (old == null)
      {
        await _logger.LogErrorAsync(
          $"Cannot update, because car with such VIN not found {fetchedData.Vin}");
        return;
      }

      if(fetchedData.Images != null && fetchedData.Images.Any())
        old.TrackingImages = fetchedData.Images
          .Select(x => new TrackingImage
          {
            ImageLink = x,
            TrackingId = old.Id
          })
          .ToList();


      old.Year = int.Parse(fetchedData.Year);
      old.Model = fetchedData.Model;
      old.Title = fetchedData.TitleStatus == "1";
      old.Keys = fetchedData.KeyStatus == "1";
      old.ExpectedArrivalDate = fetchedData.ExpectedArrivalDate;
      old.DeliveredToLoadingPlace = fetchedData.DeliveredToLoadingPlace;
      old.ContainerNumber = fetchedData.ContainerNumber;
      old.UpdatedAt = DateTime.UtcNow;

      await _context.SaveChangesAsync();
    }
  }
}
