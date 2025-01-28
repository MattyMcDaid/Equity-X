using EquityX_L00160463.Models;
using EquityX_L00160463.Services;
using EquityX_L00160463.ViewModels;
using Microcharts;
using SkiaSharp;

namespace EquityX_L00160463.View;

public partial class Stocks : ContentPage
{
    private StocksViewModel _stocksViewModel;

    public Stocks(StocksViewModel stocksViewModel)
    {
        InitializeComponent();
        _stocksViewModel = stocksViewModel;
        BindingContext = _stocksViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _stocksViewModel.LoadStockAssetsAsync();
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