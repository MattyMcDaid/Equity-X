using Microcharts;
using SkiaSharp;

namespace EquityX_L00160463.View;

public partial class SellAsset : ContentPage
{


    ChartEntry[] entries = new[]
  {
          new ChartEntry(32f)
    {

        ValueLabel = "32",
        Color = SKColor.Parse("#264653")
    },
    new ChartEntry(50f)
    {

        ValueLabel = "50",
        Color = SKColor.Parse("#2a9d8f")
    },
    new ChartEntry(80f)
    {

        ValueLabel = "80",
        Color = SKColor.Parse("#e9c46a")
    },
    new ChartEntry(100f)
    {

        ValueLabel = "100",
        Color = SKColor.Parse("#f4a261")
    },
    new ChartEntry(97f)
    {

        ValueLabel = "97",
        Color = SKColor.Parse("#e76f51")
    },
    };

    public SellAsset()
    {
        InitializeComponent();

        chartView.Chart = new LineChart()
        {
            Entries = entries
        };
    }

    private void OnPlusClicked(object sender, EventArgs e)
    {
        int currentValue = int.Parse(QuantityLabel1.Text);
        currentValue++;
        QuantityLabel1.Text = currentValue.ToString();
    }

    private void OnMinusClicked(object sender, EventArgs e)
    {
        int currentValue = int.Parse(QuantityLabel1.Text);
        currentValue--;
        QuantityLabel1.Text = currentValue.ToString();
    }
}