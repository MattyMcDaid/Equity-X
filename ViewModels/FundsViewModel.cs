using CommunityToolkit.Mvvm.Input;
using EquityX_L00160463.Models;
using EquityX_L00160463.Services;
using EquityX_L00160463.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EquityX_L00160463.ViewModels
{
    public partial class FundsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private PortfolioDBService _dbService;
        private Portfolio _portfolio;
        private int _portfolioId = 1;
        private double _totalFunds;
        private double _totalValue;
        private double _totalInvested;
        private double _profitOrLoss;
        private string _name;
        private string _address;
        private string _cardNumber;
        private string _expiryDate;
        private string _csv;
        private string _amount;
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }


        public Portfolio Portfolio
        {
            get { return _portfolio; }
            set
            {
                _portfolio = value;
                OnPropertyChanged(nameof(Portfolio));
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }

        }
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }

        }
        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                _cardNumber = value;
                OnPropertyChanged(nameof(CardNumber));
            }
        }
        public string ExpiryDate
        {
            get { return _expiryDate; }
            set
            {
                _expiryDate = value;
                OnPropertyChanged(nameof(ExpiryDate));
            }
        }
        public string CSV
        {
            get { return _csv; }
            set
            {
                _csv = value;
                OnPropertyChanged(nameof(CSV));
            }

        }
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

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

        public FundsViewModel(PortfolioDBService dBService)
        {
            _dbService = dBService;
            LoadPortfolioData();
        }



        private async void LoadPortfolioData()
        {
            var portfolio = await _dbService.GetPortfolioById(_portfolioId);
            Portfolio = portfolio;
            Funds = Portfolio.FundsAvailable;
        }

        private async void DepositAmount(Portfolio portfolio, string amountString)
        {
            double amount;
            if (double.TryParse(amountString, out amount))
            {
                if (amount > 0)
                {
                    await _dbService.DepositFunds(portfolio.Id, amount);

                }
                else
                {
                    await Shell.Current.DisplayAlert("Alert", "Please enter valid amount", "Ok");
                }
            }
            else
            {
                Debug.WriteLine("Cannot convert amount string");
            }
        }

        private async void WithdrawAmount(Portfolio portfolio, string amountString)
        {
            if (double.TryParse(amountString, out double amount))
            {
                if (amount > 0 && amount <= portfolio.FundsAvailable)
                {
                    try
                    {
                        await _dbService.WithdrawFunds(portfolio.Id, amount);
                        Funds = portfolio.FundsAvailable; // Update the funds after withdrawal
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions (e.g., insufficient funds)
                        await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Alert", "Please enter a valid amount and ensure sufficient funds", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Alert", "Invalid amount format", "OK");
            }
        }

        [RelayCommand]
        private void Withdraw()
        {
            WithdrawAmount(Portfolio, Amount);
            Funds = Portfolio.FundsAvailable;
            ResetInputFields();
        }


        private void ResetInputFields()
        {
            Amount = string.Empty;
            CardNumber = string.Empty;
            Name = string.Empty;
            ExpiryDate = string.Empty;
            CSV = string.Empty;
            Address = string.Empty;
        }

        [RelayCommand]
        async Task RefreshFundsAsync()
        {
            IsRefreshing = true;
            try
            {
                // fetch the latest funds
                var updatedFunds = await _dbService.GetFunds(_portfolioId);
                Funds = updatedFunds;
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Debug.WriteLine($"Error refreshing funds: {ex.Message}");
            }
            finally
            {
                IsRefreshing = false;
            }
        }


        [RelayCommand]
        async Task GoToAddFundsAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddFunds)}");
        }

        [RelayCommand]
        async Task GoToWithdraw()
        {
            await Shell.Current.GoToAsync($"{nameof(WithdrawFunds)}");
        }

        [RelayCommand]
        async Task GoToAddCard()
        {
            await Shell.Current.GoToAsync($"{nameof(AddCard)}");
        }

        [RelayCommand]
        private void AddAmount()
        {
            DepositAmount(Portfolio, Amount);
            Funds = Portfolio.FundsAvailable;
            ResetInputFields();
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
