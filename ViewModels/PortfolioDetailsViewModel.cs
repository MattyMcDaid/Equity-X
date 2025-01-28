using CommunityToolkit.Mvvm.Input;
using EquityX_L00160463.Models;
using EquityX_L00160463.Services;
using EquityX_L00160463.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EquityX_L00160463.ViewModels
{
    public partial class PortfolioDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private PortfolioDBService _dbService;
        private int _portfolioId;
        private double _totalFunds;
        private double _totalValue;
        private double _totalInvested;
        private double _profitOrLoss;

        public double Funds
        {
            get { return _totalFunds; }
            set
            {
                if (_totalFunds != value)
                {
                    _totalFunds = value;
                    OnPropertyChanged(nameof(Funds));
                }

            }

        }

        //public double Value { get; set; }
        public ObservableCollection<Portfolio> Portfolio { get; set; }

        public PortfolioDetailsViewModel(PortfolioDBService dBService)
        {
            _dbService = dBService;
            Portfolio = new ObservableCollection<Portfolio>();
            LoadPortfolioData();
        }

        private async void LoadPortfolioData()
        {
            var portfolio = await _dbService.GetPortfolioById(1);
            Portfolio.Add(portfolio);
            Funds = Portfolio[0].FundsAvailable;
        }

        public string FormattedFunds
        {
            get { return Funds.ToString("C", CultureInfo.CreateSpecificCulture("en-US")); }
        }

        [RelayCommand]
        async Task GoToAddFundsAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddFunds)}");
        }

        protected virtual void OnPropertyChanged(string propertyName)
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
}
