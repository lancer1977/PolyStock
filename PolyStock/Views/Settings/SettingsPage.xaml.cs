using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Settings.Abstractions;
using PolyhydraGames.Core.ViewModel;
using PolyStock.Services;
using PolyStock.Services.Settings;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PolyStock.Views.Settings
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
    }

    /// <summary>
    /// Settings
    /// Lists
    /// Save Flat
    /// Restore Flat
    /// API Key
    /// Link to generate
    ///     Entry to input
    ///     Lock this down with a toggle
    /// </summary>
    public class SettingsViewModel : ViewModelBase
    {
        private readonly IStockBasisService _stockBasis;

        public SettingsViewModel(ISettingService settings, IStockBasisService stockBasis)
        {
            _stockBasis = stockBasis;
            ApiKey = settings.StockApiKey.Value;
            this.WhenAnyValue(x => x.ApiKey).Subscribe(x => settings.StockApiKey.Value = x);
            LoadCommand = ReactiveCommand.Create(async () => await _stockBasis.Load());
            SaveCommand = ReactiveCommand.Create(async () => await _stockBasis.Save());
        }

        async Task<FileResult> PickAndShow(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
                return null;
            }
        }
        [Reactive] public string ApiKey { get; set; }
        [Reactive] public bool IsKeyUnlocked { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }

    }
}
