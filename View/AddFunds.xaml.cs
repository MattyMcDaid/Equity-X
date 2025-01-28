using EquityX_L00160463.Services;
using EquityX_L00160463.ViewModels;

namespace EquityX_L00160463.View
{

    public partial class AddFunds : ContentPage
    {

        public AddFunds() : this(new FundsViewModel(new PortfolioDBService()))
        {
        }
        public AddFunds(FundsViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }

}