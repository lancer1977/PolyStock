using System.ComponentModel;
using Plugin.Settings.Abstractions;
using PolyhydraGames.Core.Services.Settings;

namespace PolyStock.Services
{
  public interface ISettingService
    {
        StringSetting StockApiKey { get; }
    }

    public class SettingService : ISettingService
    {
        public StringSetting StockApiKey { get; }
        private const string StockKey = "STOCKS";
        private string _stock;
        public SettingService(ISettings settings)
        {
            StockApiKey = new StringSetting(settings, StockKey, Configs.AlphaVantageKey);
        }

    
    }
}