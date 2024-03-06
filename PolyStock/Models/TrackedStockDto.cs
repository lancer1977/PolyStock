using System.Collections.Generic;
using PolyStock.Services;

namespace PolyStock.Models
{
    public class TrackedStockDto 
    {
        public QuoteDto Quote { get; set; }
        public List<TransactionEvent> Events { get;  } = new List<TransactionEvent>();
        
    }
}