using EquityX_L00160463.Models;
using EquityX_L00160463.ViewModels;

namespace EquityX_L00160463.View
{
    public partial class ViewAsset : ContentPage
    {
        public ViewAsset(Asset selectedAsset)
        {
            InitializeComponent();
            var viewModel = new ViewAssetViewModel(selectedAsset);
            BindingContext = viewModel;

            chartView.Chart = viewModel.LineChart;
        }

        private async void OnBuyClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as ViewAssetViewModel;
            if (viewModel != null && viewModel.SelectedAsset != null)
            {
                await Navigation.PushAsync(new BuyAsset(viewModel.SelectedAsset));
            }
        }

    }
}
