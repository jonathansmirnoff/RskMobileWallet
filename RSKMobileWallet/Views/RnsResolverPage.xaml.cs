using System;
using System.Collections.Generic;
using RSKMobileWallet.ViewModels;
using Xamarin.Forms;

namespace RSKMobileWallet.Views
{
    public partial class RnsResolverPage : ContentPage
    {
        public string Domain { get; set; }
        //public string Address { get; set; }
        private RnsViewModel viewModel { get; set; }

        public RnsResolverPage()
        {
            InitializeComponent();

            BindingContext = this;
            viewModel = new RnsViewModel();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Domain.Trim().ToLower()))
            {
                var result = await viewModel.ResolveDomain(Domain.Trim().ToLower());

                if (result != null)
                {
                    //Address = result;
                    barcode.IsVisible = true;
                    barcode.BarcodeValue = result;
                    Addresslbl.IsVisible = true;
                    Addresslbl.Text = result;
                }
            }
        }
    }
}
