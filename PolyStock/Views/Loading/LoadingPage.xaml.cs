﻿using PolyhydraGames.Core.IOC;
using PolyStock.Setup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PolyStock.Views.Loading
{
  
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        private static LoadingViewModel _loadingViewModel;

        private static LoadingViewModel LoadingViewModel
        {
            get
            {
                if (_loadingViewModel != null) return _loadingViewModel;
                _loadingViewModel =  IOC.Get<LoadingViewModel>();
                return _loadingViewModel;
            }
        }


        public LoadingPage()
        {
            InitializeComponent();
            BindingContext = LoadingViewModel;
        }
 


    }
}