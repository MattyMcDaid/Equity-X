
using CommunityToolkit.Mvvm.Input;
using EquityX_L00160463.Models;
using EquityX_L00160463.Services;
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
    public partial class AddFundsViewModel : INotifyPropertyChanged
    {
        private Portfolio _portfolio;
        private string _name;
        private string _address;
        private string _cardNumber;
        private string _expiryDate;
        private string _csv;
        private string _amount;
        private readonly PortfolioDBService dBService;

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

        public AddFundsViewModel(PortfolioDBService portfolioDBService)
        {
            dBService = portfolioDBService;
            LoadPortfolioData();
        }

        private async void LoadPortfolioData()
        {
            var portfolio = await dBService.GetPortfolioById(1);
            Portfolio = portfolio;
        }

        private async void DepositAmount(Portfolio portfolio, string amountString)
        {
            double amount;
            if (double.TryParse(amountString, out amount))
            {
                if (amount > 0)
                {
                    await dBService.DepositFunds(portfolio.Id, amount);

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
        private void AddFunds()
        {
            DepositAmount(Portfolio, Amount);
            ResetInputFields();
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
