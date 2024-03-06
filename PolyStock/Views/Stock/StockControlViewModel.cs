using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NodaTime;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.Core.ViewModel;
using PolyStock.Models;
using PolyStock.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolyStock.Views.Stock
{
    /// <summary>
    /// This is a display of the combination of Transactions, and additional meta data about the stock itself.
    /// TransactionEvent (0+)
    /// StockDto?
    /// </summary>
    public class StockControlViewModel : ViewModelBase
    {
        //[Reactive] public TrackedStockDto Stock { get; set; }
        [Reactive] public string Symbol { get; set; } = string.Empty;
        [Reactive] public decimal OpeningPrice { get; set; }
        [Reactive] public decimal PreviousClosingPrice { get; set; }
        [Reactive] public decimal HighestPrice { get; set; }
        [Reactive] public decimal LowestPrice { get; set; }
        [Reactive] public decimal Price { get; set; }
        [Reactive] public long Volume { get; set; }
        [Reactive] public LocalDate LatestTradingDay { get; set; }

        [Reactive] public decimal Change { get; set; }
        [Reactive] public decimal ChangePercent { get; set; }


        [Reactive] public TransactionEvent LastSell { get; set; }
        [Reactive] public TransactionEvent LastBuy { get; set; }

  

        [Reactive] public int OwnedQuantity { get; set; }
        [Reactive] public decimal OwnedCostBasis { get; set; }
        [Reactive] public string Recommendation { get; set; }
        [Reactive] public TrackingType Tracking { get; set; }
        [ObservableAsProperty] public decimal OwnedCost { get; }
        [ObservableAsProperty] public decimal OwnedValue { get; }

        [ObservableAsProperty] public bool ShowCost { get; }
        [ObservableAsProperty] public bool ShowValue { get; }
        [ObservableAsProperty] public bool ShowQuantity { get; }
        [ObservableAsProperty] public string TrackingTitle { get; }


        public void ItemSelected()
        {



        }

        public void Load(QuoteDto quote, List<TransactionEvent> events, StockBasis basis)
        {
            Symbol = quote.Symbol;
            OpeningPrice = quote.OpeningPrice;
            PreviousClosingPrice = quote.PreviousClosingPrice;
            HighestPrice = quote.HighestPrice;
            LowestPrice = quote.LowestPrice;
           
            Volume = quote.Volume;
            LatestTradingDay = quote.LatestTradingDay;
            Change = quote.Change;
            ChangePercent = quote.ChangePercent;
            LastBuy = events?.LastOrDefault(x => x.Action == ActionType.Purchase);
            LastSell = events?.LastOrDefault(x => x.Action == ActionType.Sale);
            Update(basis);
            Price = quote.Price;
        }
        internal void Update(StockBasis basis)
        {
            OwnedQuantity = basis?.Quantity ?? 0;
            OwnedCostBasis = (decimal)(basis?.CostBasis ?? 0);
            Tracking = basis?.Tracking ?? TrackingType.Unknown;
        }


        public StockControlViewModel()
        {
            //this.WhenAnyValue(x => x.OwnedCostBasis, x => x.Price, (b, q) => b > 0 && b < q)
            //    .ToPropertyEx(this, x => x.IsProfit);
            //this.WhenAnyValue(x => x.OwnedCostBasis, x => x.Price, (b, q) => b > 0 && b > q)
            //    .ToPropertyEx(this, x => x.IsLoss);
            var trackingChanged = this.WhenAnyValue(x => x.Tracking).Do(x=>Debug.WriteLine(x));
            trackingChanged.Select(x=>x != TrackingType.Watching).ToPropertyEx(this, x => x.ShowCost);
            trackingChanged.Select(x => x == TrackingType.Purchased).ToPropertyEx(this, x => x.ShowQuantity);
            trackingChanged.Select(x => x == TrackingType.Purchased).ToPropertyEx(this, x => x.ShowValue);
            trackingChanged.Select(x => x.ToString()).ToPropertyEx(this, x => x.TrackingTitle);


            this.WhenAnyValue(x => x.OwnedCostBasis, x => x.OwnedQuantity, (b, q) => b * q)
                .ToPropertyEx(this, x => x.OwnedCost);

            this.WhenAnyValue(x => x.Price, x => x.OwnedQuantity, (b, q) => b * q)
                .ToPropertyEx(this, x => x.OwnedValue);
            this.WhenAnyValue(x => x.OwnedQuantity, x => x != 0).ToPropertyEx(this, x => x.ShowCost);
            this.WhenAnyValue(x => x.Price,x=>x.OwnedCostBasis).Where(x=>x.Item2 > 0).Subscribe(x =>
            {
                var price = x.Item1;
                var basis = x.Item2; 
                var change = (price / basis);
                Debug.WriteLine($"Symbol: {Symbol} Change: {change}");
                Recommendation = "Hold";
                if (OwnedQuantity > 0)
                { 
                    if (change > (decimal)1.10)
                        Recommendation = "Sell";
                    else
                        if (change < (decimal).90)
                        Recommendation = "Buy";
                }
                if (OwnedQuantity == -1)
                {
               
                    if (change < (decimal).90)
                        Recommendation = "Buy";
                }

            });
        }

        public static async Task<StockControlViewModel> GetNew(string symbol, List<TransactionEvent> events)
        {
            var reader = IOC.Get<IStockReaderService>();
            var quote = await reader.GetStockAsync(symbol);
            var trackedStockDto = new TrackedStockDto()
            {
                Quote = quote,
            };
            trackedStockDto.Events.AddRange(events);

            var transaction = new StockControlViewModel();
            transaction.Load(trackedStockDto.Quote, trackedStockDto.Events, null);
            return transaction;
        }

        public static async Task<StockControlViewModel> GetNew(string symbol, StockBasis basis)
        {
            var reader = IOC.Get<IStockReaderService>();
            var quote = await reader.GetStockAsync(symbol);
            var trackedStockDto = new TrackedStockDto()
            {
                Quote = quote,
            }; 

            var transaction = new StockControlViewModel();
            transaction.Load(trackedStockDto.Quote, null, basis);
            return transaction;
        }


    }
}