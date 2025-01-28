using EquityX_L00160463.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using EquityX_L00160463.Services;

namespace EquityX_L00160463.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly LocalDbService _dbService;
        private int _editBankID = 0;
        private string _cardNumber;
        private string _cardName;
        private string _expDate;
        private int _cvv;
        private string _billingAdd;

        public ObservableCollection<BankInfo> BankInfos { get; private set; }
        public ICommand AddOrUpdateCommand { get; private set; }
        public ICommand ItemTappedCommand { get; private set; }

        public string CardName
        {
            get => _cardName;
            set => SetProperty(ref _cardName, value);
        }



        public string CardNumber
        {
            get => _cardNumber;
            set => SetProperty(ref _cardNumber, value);
        }



        public string ExpDate
        {
            get => _expDate;
            set => SetProperty(ref _expDate, value);
        }

        public int CVV
        {
            get => _cvv;
            set => SetProperty(ref _cvv, value);
        }

        public string BillingAdd
        {
            get => _billingAdd;
            set => SetProperty(ref _billingAdd, value);
        }



        public MainPageViewModel(LocalDbService dbService)
        {
            _dbService = dbService;
            BankInfos = new ObservableCollection<BankInfo>();
            AddOrUpdateCommand = new Command(async () => await AddOrUpdateBankInfo());
            ItemTappedCommand = new Command<BankInfo>(async (item) => await OnItemTapped(item));
            LoadBankInfos();
        }

        private async void LoadBankInfos()
        {
            var bankInfos = await _dbService.GetBankInfo();
            BankInfos.Clear();
            foreach (var info in bankInfos)
            {
                BankInfos.Add(info);
            }
        }

        private async Task AddOrUpdateBankInfo()
        {
            var bankInfo = new BankInfo
            {
                CardNumber = CardNumber,
                CardName = CardName,
                ExpDate = ExpDate,
                CVV = CVV,
                BillingAdd = BillingAdd,
                Id = _editBankID
            };

            if (_editBankID == 0)
            {
                await _dbService.Create(bankInfo);
            }
            else
            {
                await _dbService.Update(bankInfo);
                _editBankID = 0;
            }

            ResetInputFields();
            LoadBankInfos();
        }

        public async Task OnItemTapped(BankInfo bankInfo)
        {
            var action = await Application.Current.MainPage.DisplayActionSheet("Options", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    EditBankInfo(bankInfo);
                    break;
                case "Delete":
                    await _dbService.Delete(bankInfo);
                    LoadBankInfos();
                    break;
            }
        }

        private void EditBankInfo(BankInfo bankInfo)
        {
            _editBankID = bankInfo.Id;
            CardNumber = bankInfo.CardNumber;
            CardName = bankInfo.CardName;
            ExpDate = bankInfo.ExpDate;
            CVV = bankInfo.CVV;
            BillingAdd = bankInfo.BillingAdd;

        }

        private void ResetInputFields()
        {
            CardNumber = string.Empty;
            CardName = string.Empty;
            ExpDate = string.Empty;
            CVV = 0;
            BillingAdd = string.Empty;


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
}
