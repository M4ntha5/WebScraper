using System;

namespace WebScraper.Models
{
  public class Log
  {
    public int Id { get; set; }
    public int Level { get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; }
  }
}
