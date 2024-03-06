using System.ComponentModel;
using Xamarin.Forms;

namespace PolyStock.Views.Stock
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class StockPage : ContentPage
    {

        public StockViewModel ViewModel => (StockViewModel)BindingContext;
        public StockPage()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.UWP)
            {
 
                //< ToolbarItem Text = " + "  Command = "{Binding DateUpCommand}" />
   
                  //  < ToolbarItem   Text = " - "  Command = "{Binding DateDownCommand}" />
            }
        }

        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var vm = e.SelectedItem as StockControlViewModel;
            (sender as ListView).SelectedItem = null;
            ViewModel.ItemSelected(vm);
  

        }
    }
}