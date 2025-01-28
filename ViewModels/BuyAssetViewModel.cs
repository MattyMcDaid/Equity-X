using EquityX_L00160463.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace EquityX_L00160463.ViewModels
{
    public class BuyAssetViewModel : INotifyPropertyChanged
    {
        private Asset _selectedAsset;
        private int _quantity;
        private ObservableCollection<AssetPurchase> _purchases = new ObservableCollection<AssetPurchase>();

        public Asset SelectedAsset
        {
            get => _selectedAsset;
            set => SetProperty(ref _selectedAsset, value);
        }

        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        public ObservableCollection<AssetPurchase> Purchases
        {
            get => _purchases;
            private set => SetProperty(ref _purchases, value);
        }

        public ICommand BuyCommand { get; private set; }

        public BuyAssetViewModel(Asset selectedAsset)
        {
            SelectedAsset = selectedAsset;
            BuyCommand = new Command(ExecuteBuyCommand);
        }

        private void ExecuteBuyCommand()
        {
            var purchase = new AssetPurchase
            {
                AssetName = SelectedAsset.ShortName,
                Price = SelectedAsset.RegularMarketPrice,
                Quantity = Quantity,
            };

            Purchases.Add(purchase);
            ResetInputFields();
        }

        private void ResetInputFields()
        {
            Quantity = 0; // Reset quantity
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public class AssetPurchase
    {
        public string AssetName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
