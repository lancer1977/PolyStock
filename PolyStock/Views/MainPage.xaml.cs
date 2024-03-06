using System.ComponentModel;
using System.Threading.Tasks;
using PolyhydraGames.Core.Interfaces;
using PolyhydraGames.Core.IOC;
using PolyhydraGames.Core.ViewModel;
using PolyStock.Views.About;
using PolyStock.Views.Loading;
using PolyStock.Views.Settings;
using PolyStock.Views.Stock;
using Xamarin.Forms;

namespace PolyStock.Views
{

    public class MainViewModel : ViewModelBase
    {

    }
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private INavigator _navigator;
        public MainPage()
        {
            InitializeComponent();
            _navigator = IOC.Get<INavigator>();
            MasterBehavior = MasterBehavior.Popover; 

        }

        public async void NavigateFromMenu(string id)
        {
            //if (Device.RuntimePlatform == Device.Android)
            //    await Task.Delay(100);
            switch (id)
            { 
                case "Reload Data":
                    await _navigator.SetRoot<LoadingViewModel>();
                    
                    //DependencyService.Get<ICovidSource>().Load();
                    break;
                case "Stocks":
                    await _navigator.NavigateTo<StockViewModel>(async x => await x.LoadAsync()); 
                    break;
                case "Settings":
                    await _navigator.NavigateTo<SettingsViewModel>();
                    //Detail = new NavigationPage(new ChangeChartPage());
                    break;
                case "About":
                    Detail = new NavigationPage(new AboutPage());
                    break;
                default:
                    if (id.Contains("-----")) return; 
                    break;
            }


            if (Device.RuntimePlatform != Device.WPF)
                IsPresented = false;
        }


    }
}