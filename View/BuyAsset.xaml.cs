using EquityX_L00160463.Models;
using EquityX_L00160463.ViewModels;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;

namespace EquityX_L00160463.View;

public partial class BuyAsset : ContentPage
{





    public BuyAsset(Asset selectedAsset)
    {
        InitializeComponent();

        var viewModel = new BuyAssetViewModel(selectedAsset);
        BindingContext = viewModel;


    }



}