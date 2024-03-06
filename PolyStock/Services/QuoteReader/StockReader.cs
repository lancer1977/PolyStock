using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AlphaVantage.Net.Core.Client;
using AlphaVantage.Net.Stocks;
using AlphaVantage.Net.Stocks.Client;

namespace PolyStock.Services
{
    public class StockReaderService :IStockReaderService
    {
        static Dictionary<string, QuoteDto> _quotes = new Dictionary<string, QuoteDto>();
        private readonly ISettingService _settingService;

        public StockReaderService(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public   async Task<QuoteDto> GetStockAsync(string name)
        {
            if (_quotes.ContainsKey(name))
            {
                if (_quotes[name].Time > DateTime.Now - TimeSpan.FromMinutes(15))
                {
                    return _quotes[name];
                }
            }
            // use your AlphaVantage API key
            string apiKey = _settingService.StockApiKey.Value;
            // there are 5 more constructors available
            using var client = new AlphaVantageClient(apiKey);
            using var stocksClient = client.Stocks();

            // StockTimeSeries stockTs = await stocksClient.GetTimeSeriesAsync(name, Interval.Daily, OutputSize.Compact, isAdjusted: true);
            try
            {
                GlobalQuote globalQuote = await stocksClient.GetGlobalQuoteAsync(name);

                var converted = GetQuoteDto(globalQuote);
                _quotes.Add(name, converted);
                return converted;
            }
            catch(Exception ex) 
            {
                Debug.WriteLine(ex.Message);
                return new QuoteDto();
            }

            //ICollection<SymbolSearchMatch> searchMatches = await stocksClient.SearchSymbolAsync(name);
     
    
        }

        public static  QuoteDto GetQuoteDto(GlobalQuote Quote)
        {

            return new QuoteDto()
            {
                Symbol = Quote.Symbol,
                OpeningPrice = Quote.OpeningPrice,
                PreviousClosingPrice = Quote.PreviousClosingPrice,
                HighestPrice = Quote.HighestPrice,
                LowestPrice = Quote.LowestPrice,
                Price = Quote.Price,
                Volume = Quote.Volume,
                LatestTradingDay = Quote.LatestTradingDay,
                Change = Quote.Change,
                ChangePercent = Quote.ChangePercent,
            };
        }

    }
}