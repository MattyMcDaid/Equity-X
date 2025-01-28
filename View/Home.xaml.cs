using EquityX_L00160463.Models;
using EquityX_L00160463.Services;
using EquityX_L00160463.ViewModels;

namespace EquityX_L00160463.View;

public partial class Home : ContentPage
{
    private HomePageViewModel _homePageViewModel;

    public Home(HomePageViewModel viewModel)
    {
        InitializeComponent();
        _homePageViewModel = viewModel;
        BindingContext = _homePageViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _homePageViewModel.LoadTopAssetsByChangePercentAsync();
    }

    private async void OnViewClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.BindingContext is Asset selectedAsset)
        {
            await Navigation.PushAsync(new ViewAsset(selectedAsset));
        }
    }



    private void OnViewPortfolioClicked(object sender, EventArgs e)
    {
        // Use navigation to go to the target page
        Navigation.PushAsync(new PortfolioPage());
    }
    private void OnAddMoneyClicked(object sender, EventArgs e)
    {
        // Use navigation to go to the target page
        Navigation.PushAsync(new AddFunds());
    }









}