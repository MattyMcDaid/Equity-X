using EquityX_L00160463.Models;
using EquityX_L00160463.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EquityX_L00160463.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private readonly IAssetDataService _assetDataService;
        private ObservableCollection<Asset> _topAssetsByChange;
        private FundsViewModel _fundsViewModel;


        public ObservableCollection<Asset> TopAssetsByChange
        {
            get => _topAssetsByChange;
            set
            {
                _topAssetsByChange = value;
                OnPropertyChanged(nameof(TopAssetsByChange));
            }
        }

        // Property to access FundsViewModel
        public FundsViewModel FundsVM
        {
            get => _fundsViewModel;
            private set
            {
                _fundsViewModel = value;
                OnPropertyChanged(nameof(FundsVM));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ViewAssetCommand { get; private set; }

        public HomePageViewModel(IAssetDataService assetDataService, PortfolioDBService dbService)
        {
            _assetDataService = assetDataService;
            _topAssetsByChange = new ObservableCollection<Asset>();
            FundsVM = new FundsViewModel(dbService);
            Task task = LoadTopAssetsByChangePercentAsync();
        }





        public async Task LoadTopAssetsByChangePercentAsync()
        {
            var assets = await _assetDataService.GetAssetsData();
            var topAssets = assets.OrderByDescending(asset => Math.Abs(asset.RegularMarketChangePercent))
                                  .Take(3)
                                  .ToList();

            TopAssetsByChange.Clear();
            foreach (var asset in topAssets)
            {
                TopAssetsByChange.Add(asset);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
