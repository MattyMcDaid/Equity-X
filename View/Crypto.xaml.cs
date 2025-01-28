using EquityX_L00160463.Models;
using EquityX_L00160463.ViewModels;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;

namespace EquityX_L00160463.View;


//Data for the chart this was on a youtub video I watched heres the link https://www.youtube.com/watch?v=yMG8oPIuMig&ab_channel=GeraldVersluis
public partial class Crypto : ContentPage
{
    private CryptoViewModel _cryptoViewModel;

    public Crypto(CryptoViewModel stocksViewModel)
    {
        InitializeComponent();
        _cryptoViewModel = stocksViewModel;
        BindingContext = _cryptoViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _cryptoViewModel.LoadStockAssetsAsync();
    }

    private async void OnViewClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.BindingContext is Asset selectedAsset)
        {
            await Navigation.PushAsync(new ViewAsset(selectedAsset));
        }
    }

}