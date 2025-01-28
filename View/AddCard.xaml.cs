using System.Collections.ObjectModel;
using EquityX_L00160463.Models;
using EquityX_L00160463.Services;
using EquityX_L00160463.ViewModels;
using Microsoft.Maui.Controls;

namespace EquityX_L00160463.View;

public partial class AddCard : ContentPage
{


    public AddCard() : this(new LocalDbService())
    {
    }


    public AddCard(LocalDbService dbService)
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel(dbService);

    }

    private async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is BankInfo bankInfo && BindingContext is MainPageViewModel viewModel)
        {
            await viewModel.OnItemTapped(bankInfo);
        }
    }
}
