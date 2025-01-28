
using EquityX_L00160463.Services;
using EquityX_L00160463.ViewModels;


namespace EquityX_L00160463.View;

public partial class WithdrawFunds : ContentPage
{

    public WithdrawFunds() : this(new FundsViewModel(new PortfolioDBService()))
    {
        // This constructor will be used by the framework
    }
    public WithdrawFunds(FundsViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}