using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.IOC;
using PolyStock.Models;
using PolyStock.Services;
using PolyStock.Views.Abstract.PFAssistant.Core.Views.Abstracts;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolyStock.Views.Stock.AddBasis
{
    public class AddBasisViewModel : ModalViewModelBase<StockBasis>
    {
        private StockBasis _originalStock;
        public AddBasisViewModel(INavigator navigator) : base(navigator)
        {
            Title = "Stock ";
            this.WhenAnyValue(x => x.IsSell).Select(x => x ? ActionType.Sale : ActionType.Purchase)
                .ToPropertyEx(this, x => x.TransactionType, ActionType.Purchase);
            var transactionChanged = this.WhenAnyValue(x => x.Tracking);
            transactionChanged.Select(x => x != TrackingType.Watching).ToPropertyEx(this, x => x.ShowPrice);
            transactionChanged.Select(x => x == TrackingType.Purchased).ToPropertyEx(this, x => x.ShowQuantity);
        }


        public override void Load(Action<StockBasis> value)
        {
            base.Load(value);
        }
        public void Load(Action<StockBasis> value, StockBasis oldStock)
        {
            base.Load(value);
            _originalStock = oldStock;
            Price = _originalStock.CostBasis;
            Quantity = oldStock.Quantity;
            Symbol = _originalStock.Symbol;
            CanEditSymbol = false;
            Tracking = oldStock.Tracking;
        }


        public override bool IsValid()
        {
            return true; // return Verify(Symbol) && Verify(Quantity) && Verify(Size);

        }

        protected override async Task<bool> OnOK()
        {
            var stock = _originalStock ?? new StockBasis()
            {
                Symbol = Symbol.ToUpper(),
                Tracking = Tracking
            };
            switch (Tracking)
            {
                case TrackingType.Purchased:
                    stock.CostBasis = Price;
                    stock.Quantity = Quantity;
                    break;
                case TrackingType.Sold:
                    stock.CostBasis = Price;
                    stock.Quantity = -1;
                    break;
                case TrackingType.Watching:
                    stock.CostBasis = 0;
                    stock.Quantity = 0;
                    break;

            }

            OkAction(stock);
            return true;
        }

        [Reactive] public TrackingType Tracking { get; set; }
        [Reactive] public bool IsSell { get; set; }
        [Reactive] public string Symbol { get; set; }
        [Reactive] public int Quantity { get; set; }
        [Reactive] public double Price { get; set; }
        [Reactive] public DateTime TransactionDate { get; set; }
        [ObservableAsProperty] public ActionType TransactionType { get; }
        [ObservableAsProperty] public bool ShowPrice { get; }
        [ObservableAsProperty] public bool ShowQuantity { get; }
        [Reactive] public bool CanEditSymbol { get; set; } = true;

        public static async Task<StockBasis> GetBasis()
        {
            var tcs = new TaskCompletionSource<StockBasis>();
            var nav = IOC.Get<INavigator>();
            await nav.PushPopupAsync<AddBasisViewModel>(async x =>
            {
                x.Load(y => tcs.SetResult(y));

            });

            return await tcs.Task;
        }

        public static async Task<StockBasis> UpdateBasis(StockBasis basis)
        {
            var tcs = new TaskCompletionSource<StockBasis>();
            var nav = IOC.Get<INavigator>();
            await nav.PushPopupAsync<AddBasisViewModel>(async x =>
            {
                x.Load(y => tcs.SetResult(y), basis);

            });

            return await tcs.Task;
        }
    }
}