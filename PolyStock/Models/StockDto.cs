using System;
using System.Text.Json.Serialization;
using NodaTime;

namespace PolyStock.Services
{
 

    public class QuoteDto
    {
        public string Symbol { get; set; } = string.Empty; 
        public decimal OpeningPrice { get; set; } 
        public decimal PreviousClosingPrice { get; set; } 
        public decimal HighestPrice { get; set; } 
        public decimal LowestPrice { get; set; } 
        public decimal Price { get; set; } 
        public long Volume { get; set; } 
        public LocalDate LatestTradingDay { get; set; }
        public decimal Change { get; set; } 
        public decimal ChangePercent { get; set; }
        public DateTime Time { get; }

        public QuoteDto()
        {
            Time = DateTime.Now;
        }
    }
}