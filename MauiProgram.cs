using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using EquityX_L00160463.Services;
using EquityX_L00160463.ViewModels;
using EquityX_L00160463.View;

namespace EquityX_L00160463
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IAssetDataService, AssetDataService>();

            builder.Services.AddTransient<AssetViewModel>();

            builder.Services.AddTransient<HomePageViewModel>();

            builder.Services.AddTransient<ViewAssetViewModel>();

            builder.Services.AddTransient<StocksViewModel>();

            builder.Services.AddTransient<CryptoViewModel>();

            builder.Services.AddTransient<Home>();

            builder.Services.AddTransient<ViewAsset>();

            builder.Services.AddTransient<Stocks>();

            builder.Services.AddTransient<Crypto>();

            builder.Services.AddTransient<WithdrawFunds>();

            builder.Services.AddSingleton<IAssetDataService, AssetDataService>();

            builder.Services.AddSingleton<LocalDbService>();

            builder.Services.AddSingleton<PortfolioDBService>();

            // Register ViewModels
            builder.Services.AddTransient<MainPageViewModel>();
            //builder.Services.AddTransient<PortfolioDetailsViewModel>();
            builder.Services.AddTransient<FundsViewModel>();
            builder.Services.AddTransient<AddFundsViewModel>();
            builder.Services.AddTransient<WithdrawFunds>();





#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}