using System.Diagnostics;
using PolyStock.Interfaces;
using Xamarin.Forms;
using PolyStock.Views;
using PolyhydraGames.Core.Forms;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter;
using Xamarin.Forms.Xaml;

namespace PolyStock
{
    public partial class App : BaseApplication
    {

        public App()
        {

            InitializeComponent();
            AppCenter.Start(Configs.AppCenterSecret, typeof(Crashes), typeof(Distribute));
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Configs.SyncfusionKey);
            HttpService.Instance = new HttpService();
            MainPage = new MainPage(); ;
            foreach (var item in Application.Current.Resources.Keys)
                Debug.WriteLine(item);

        }



        public void GotoMainPage()
        {
            if (MainPage is MainPage)
            {

            }
            else
            {

                MainPage = new MainPage();
            }
        }
    }
}
