﻿using System.Windows.Input;
using EquityX_L00160463.Models;
using EquityX_L00160463.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using EquityX_L00160463.View;

namespace EquityX_L00160463.ViewModels
{
    public class CryptoViewModel : INotifyPropertyChanged
    {
        private readonly IAssetDataService _assetDataService;
        private ObservableCollection<Asset> _stocks;
        private Asset _selectedStock;

        public ObservableCollection<Asset> Stocks
        {
            get => _stocks;
            set
            {
                _stocks = value;
                OnPropertyChanged(nameof(Stocks));
            }
        }

        public Asset SelectedStock
        {
            get => _selectedStock;
            set
            {
                _selectedStock = value;
                OnPropertyChanged(nameof(SelectedStock));
            }
        }

        public ICommand BuyStockCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CryptoViewModel(IAssetDataService assetDataService)
        {
            _assetDataService = assetDataService;
            Stocks = new ObservableCollection<Asset>();
            BuyStockCommand = new Command<Asset>(ExecuteBuyStockCommand);
            _ = LoadStockAssetsAsync();
        }

        private async void ExecuteBuyStockCommand(Asset selectedStock)
        {
            await Shell.Current.GoToAsync(nameof(BuyAsset));
        }

        public async Task LoadStockAssetsAsync()
        {
            var allAssets = await _assetDataService.GetAssetsData();
            var CrytpoAssets = allAssets.Where(asset => asset.QuoteType == "CRYPTOCURRENCY").ToList();

            Stocks.Clear();
            foreach (var asset in CrytpoAssets)
            {
                Stocks.Add(asset);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
