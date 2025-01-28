using System.ComponentModel;
using EquityX_L00160463.Models;
using Microcharts;
using SkiaSharp;
using System.Windows.Input; // Required for ICommand
using Microsoft.Maui.Controls; // Required for Command
using System.Text.Json; // Required for serialization

namespace EquityX_L00160463.ViewModels
{
    public class ViewAssetViewModel : INotifyPropertyChanged
    {
        private Asset _selectedAsset;
        private LineChart _lineChart;

        public Asset SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                _selectedAsset = value;
                OnPropertyChanged(nameof(SelectedAsset));
                UpdateLineChart(); // Call to update the chart when the asset changes
            }
        }

        public LineChart LineChart
        {
            get => _lineChart;
            set
            {
                _lineChart = value;
                OnPropertyChanged(nameof(LineChart));
            }
        }

        public ICommand BuyAssetCommand { get; private set; } // Command for navigation

        public ViewAssetViewModel(Asset asset)
        {
            SelectedAsset = asset;
            BuyAssetCommand = new Command(NavigateToBuyAsset); // Initialize command

            // Initialize the LineChart based on the selected asset
            UpdateLineChart();
        }

        private async void NavigateToBuyAsset()
        {
            var assetJson = JsonSerializer.Serialize(SelectedAsset);

            var encodedAssetJson = Uri.EscapeDataString(assetJson);

            var route = $"//buyasset";
            await Shell.Current.GoToAsync($"{route}?assetJson={encodedAssetJson}");
        }

        private void UpdateLineChart()
        {
            // Generate chart entries based on SelectedAsset
            var entries = new[]
            {
                new ChartEntry((float)SelectedAsset.FiftyTwoWeekLow)
             {
                 Label = "52 Week Low",
                 ValueLabel = SelectedAsset.FiftyTwoWeekLow.ToString(),
                 Color = SKColor.Parse("#264653")
             },
             new ChartEntry((float)SelectedAsset.FiftyTwoWeekHigh)
             {
                 Label = "52 Week High",
                 ValueLabel = SelectedAsset.FiftyTwoWeekHigh.ToString(),
                 Color = SKColor.Parse("#2a9d8f")
             },
             new ChartEntry((float)SelectedAsset.RegularMarketPrice)
             {
                 Label = "Market Price",
                 ValueLabel = SelectedAsset.RegularMarketPrice.ToString(),
                 Color = SKColor.Parse("#e9c46a")
             },
            };

            LineChart = new LineChart { Entries = entries };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
