using PolyStock.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using PolyStock.Interfaces;
using PolyStock.Setup;
using ReactiveUI;
using Xamarin.Forms;
using PolyhydraGames.Core.ViewModel;
using PolyhydraGames.Core.Forms.Controls;

namespace PolyStock.Views
{
    public class MenuViewModel : ViewModelBase
    {

    }
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : PageBase
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; } 
        ObservableCollection<HomeMenuItem> menuItems = new ObservableCollection<HomeMenuItem>();

        public MenuPage()
        {
            InitializeComponent();


            menuItems.AddRange(new[]{
                  new HomeMenuItem() {Title = "-------"},
            new HomeMenuItem() {Title = "Stocks"},
            new HomeMenuItem() {Title = "Settings"},

            });
 
 
            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                // var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                RootPage.NavigateFromMenu(e.SelectedItem.ToString());
            };
        }


    }
}