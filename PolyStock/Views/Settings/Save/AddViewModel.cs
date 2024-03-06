




using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using PolyhydraGames.Core.Forms.Navigation;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.IOC;
using PolyStock.Models;
using PolyStock.Services;
using PolyStock.Views.Abstract.PFAssistant.Core.Views.Abstracts;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolyStock.Views.Stock.Add
{
    public class AddViewModel : ModalViewModelBase<TransactionEvent>
    {

        public AddViewModel(INavigator navigator) : base(navigator)
        {
            Title = "Stock ";
            this.WhenAnyValue(x => x.IsSell).Select(x => x ? ActionType.Sale : ActionType.Purchase)
                .ToPropertyEx(this, x => x.TransactionType, ActionType.Purchase);
        }


        public override void Load(Action<TransactionEvent> value)
        {
            base.Load(value);
        }

        public override bool IsValid()
        {
            return true; // return Verify(Symbol) && Verify(Quantity) && Verify(Size);

        }

        protected override async Task<bool> OnOK()
        {
            var stock = new TransactionEvent()
            {
                Symbol = Symbol.ToUpper(),
                Date = TransactionDate,
                Quantity = Quantity,
                Price = Price,
                Action = TransactionType
            };
            OkAction(stock);
            return true;
        }

        [Reactive] public bool IsSell { get; set; }
        [Reactive] public string Symbol { get; set; }
        [Reactive] public int Quantity { get; set; }
        [Reactive] public double Price { get; set; }
        [Reactive] public DateTime TransactionDate { get; set; }
        [ObservableAsProperty] public ActionType TransactionType { get; }


        public static async Task<TransactionEvent> GetTransactionEvent()
        {
            var tcs = new TaskCompletionSource<TransactionEvent>();
            var nav = IOC.Get<INavigator>();
            await nav.PushPopupAsync<AddViewModel>(async x =>
            {
                x.Load(y => tcs.SetResult(y));

            });

            return await tcs.Task;
        }
    }
}