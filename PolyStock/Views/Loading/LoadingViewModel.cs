using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using PolyhydraGames.Core.Forms.Interfaces;
using PolyStock.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace PolyStock.Views.Loading
{
    public class LoadingViewModel : PolyhydraGames.Core.ViewModel.ViewModelBase, IFirstPageViewModel
    {
        public List<string> FontNames { get; } = new List<string>() { "Default", "FuturaBold", "FuturaMedium", "ProximaNovaRegular" };
        [Reactive] public string Font { get; set; } = "Default";
        private static bool _runOnce;


        public ReactiveCommand<Unit, Task> LaunchNyTimesCommand { get; set; }
        [Reactive] public string TimerText { get; set; }
        [Reactive] public DateTime LatestDay { get; set; }


        Stopwatch _stopwatch = new Stopwatch();
        public string Version => Configs.Version;

        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public bool ShowRefresh { [ObservableAsProperty] get; }
        public ReactiveCommand<Unit, Unit> LoadCommand;
        public ReactiveCommand<Unit, QuoteDto> TestCommand { get; }
 
        public LoadingViewModel(IStockReaderService stockReader)
        {
            Title = "Loading ...";
 
          
            TestCommand = ReactiveCommand.CreateFromTask(async () => await stockReader.GetStockAsync("EXPR"));
            TestCommand.ThrownExceptions.Subscribe(OnException);
        }

 
        private void OnException(Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        public void OnAppearing()
        { 
        }
    }
}