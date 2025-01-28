
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
namespace EquityX_L00160463.View;

public partial class PortfolioPage : ContentPage
{

    //DATA FOR THE CHART

    ChartEntry[] entries = new[]
   {
          new ChartEntry(32f)
    {
        Label = "Amazon",
        ValueLabel = "150.25",
        Color = SKColor.Parse("#264653")
    },
    new ChartEntry(50f)
    {
        Label = "DodgeCoin",
        ValueLabel = "153.50",
        Color = SKColor.Parse("#2a9d8f")
    },
    new ChartEntry(155f)
    {
        Label = "Github",
        ValueLabel = "148.75",
        Color = SKColor.Parse("#e9c46a")
    },
    new ChartEntry(100f)
    {
        Label = "BitCoin",
        ValueLabel = "155.00",
        Color = SKColor.Parse("#f4a261")
    },
    new ChartEntry(97f)
    {
        Label = "Facebook",
        ValueLabel = "160.75",
        Color = SKColor.Parse("#e76f51")
    },

      new ChartEntry(97f)
    {
        Label = "Twitter",
        ValueLabel = "160.75",
        Color = SKColor.Parse("#e76f51")
    }
    };


    public PortfolioPage()
    {
        InitializeComponent();


        chartView.Chart = new DonutChart()
        {
            Entries = entries
        };



    }

    private void SellAssetClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SellAsset());
    }
}