using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.Core.ViewModel;
using PolyStock.Models;
using PolyStock.Services;
using PolyStock.Views.Stock.Add;
using PolyStock.Views.Stock.AddBasis;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolyStock.Views.Stock
{
    /// <summary>
    /// Stock LIST
    /// </summary>
    public class StockViewModel : ViewModelBase
    {
        private readonly IStockBasisService _stockService;
        public ICommand AddCommand { get; }
        public ICommand SortCommand { get; }
        [Reactive] public Sort Sort { get; set; }
        private readonly SourceList<StockControlViewModel> _sourceViewModels;
        private readonly ReadOnlyObservableCollection<StockControlViewModel> _viewModels;
        public ReadOnlyObservableCollection<StockControlViewModel> ViewModels => _viewModels;

        internal async void ItemSelected(StockControlViewModel vm)
        {
            var basis = _stockService.GetBasis(vm.Symbol);

await            AddBasisViewModel.UpdateBasis(basis);
            _stockService.Update(basis);
            vm.Update(basis);
        }

        INavigator Navigator => IOC.Get<INavigator>();

        public StockViewModel(IStockBasisService stockService)
        {
            _stockService = stockService;
            Title = "NA";
            _sourceViewModels = new SourceList<StockControlViewModel>();
            //AddCommand = ReactiveCommand.Create(async () =>
            //{
            //    var response = await AddBasisViewModel.GetTransaction();
            //    stockService.Insert(response);
            //    var events = stockService.GetEventLists(response.Symbol);
            //    var oldVm = _sourceViewModels.Items.FirstOrDefault(x => x.Symbol == response.Symbol);
            //    if (oldVm == null)
            //    {

            //        await Insert(response.Symbol, events);
            //    }
            //    else
            //    {
            //        await Update(oldVm, response.Symbol, events);
            //    }
            //});

            AddCommand = ReactiveCommand.Create(async () =>
            {
                //var oldItem = stockService.GetBasis(response.Symbol);
               // await AddViewModel.GetTransactionEvent();
                var response = await AddBasisViewModel.GetBasis();
                stockService.Insert(response);
                
                var oldVm = _sourceViewModels.Items.FirstOrDefault(x => x.Symbol == response.Symbol);
                if (oldVm == null)
                {

                    await Insert(response);
                }
                else
                {
                    await Update(oldVm, response);
                }
            });
            //var sortChanged = this.WhenAnyValue(x => x.Sort).Select(x => x.GetSorter());
            _sourceViewModels.Connect()
                //         .Sort(sortChanged)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _viewModels)
                .Subscribe();

            SortCommand = ReactiveCommand.Create<string, Unit>((x) =>
            {
                Sort = x.GetSort(Sort);
                return Unit.Default;
            });



        }
        private async Task Update(StockControlViewModel old, string symbol, List<TransactionEvent> events)
        {
            _sourceViewModels.Remove(old);
            await Insert(symbol, events);
        }
        private async Task Insert(string symbol, List<TransactionEvent> events)
        {
            var vm = await StockControlViewModel.GetNew(symbol, events);
            _sourceViewModels.Add(vm);
        }


        private async Task Update(StockControlViewModel old,  StockBasis events)
        {
            _sourceViewModels.Remove(old);
            await Insert(events);
        }
        private async Task Insert(StockBasis events)
        {
            var vm = await StockControlViewModel.GetNew(events.Symbol, events);
            _sourceViewModels.Add(vm);
        }


        public async Task LoadAsync()
        {
            var lists = _stockService.GetAll();
            _sourceViewModels.Clear();
            foreach (var item in lists)
            {
                await Insert(item);
            }
        }

    }
}