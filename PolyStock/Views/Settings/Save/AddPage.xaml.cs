using PolyStock.Views.Stock.AddBasis;
using Rg.Plugins.Popup.Extensions;
namespace PolyStock.Views.Stock.Add
{
	public partial class AddPage
	{
		public AddPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing(); 
		}
 

 
		//This will force the pop to close when the background area is clicked
		protected override bool OnBackgroundClicked()
		{
			CloseAllPopup();
			((AddBasisViewModel)ViewModel).CancelCommand.Execute(null);
			return false;
		}

		private async void CloseAllPopup()
		{
			await Navigation.PopAllPopupAsync(false);
		}


	}
}
