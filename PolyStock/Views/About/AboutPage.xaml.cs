using PolyhydraGames.Core.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PolyStock.Views.About
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
     
        public AboutPage()
        {
            InitializeComponent();
        }
         
    }

    public class AboutViewModel : ViewModelBase
    {

    }
}