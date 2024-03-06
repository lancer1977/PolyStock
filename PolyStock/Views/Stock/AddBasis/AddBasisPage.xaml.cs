using Rg.Plugins.Popup.Extensions;

namespace PolyStock.Views.Stock.AddBasis
{
	public partial class AddBasisPage
	{
		public AddBasisPage()
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
