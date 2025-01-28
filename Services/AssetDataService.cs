using EquityX_L00160463.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace EquityX_L00160463.Services
{
    public class AssetDataService : IAssetDataService
    {
        private readonly string _apiBaseUrl = "https://yfapi.net/v6/finance/quote";
        private readonly string API_KEY = "HFxyRZz4cc73DYba7hkQD5EQUsIidi1gaYpt3AQW";
        private readonly HttpClient _httpClient;

        public AssetDataService()
        {
            _httpClient = new HttpClient();
        }

        // Shared method to fetch assets data based on symbols
        private async Task<List<Asset>> FetchAssetsData(string symbols)
        {
            var assetQuotes = new List<Asset>();
            try
            {
                var requestUri = $"{_apiBaseUrl}?region=US&lang=en&symbols={symbols}";
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                request.Headers.Add("X-API-KEY", API_KEY);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseBody);

                assetQuotes = ConvertToAssetObjects(myDeserializedClass);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching asset data: {ex.Message}");
            }
            return assetQuotes;
        }

        public async Task<List<Asset>> GetFeaturedAssetsData()
        {
            var featuredSymbols = "META,AMZN,MSFT,TSLA,AAPL,BTC-USD,ETH-USD,USDT-USD,BNB-USD,SOL-USD,XRP-USD,DOGE-USD,SHIB-USD,XRP-USD"; // Example symbols for featured assets
            return await FetchAssetsData(featuredSymbols);
        }

        public async Task<List<Asset>> GetAssetsData()
        {
            return await FetchAssetsData("META,AMZN,MSFT,TSLA,AAPL,BTC-USD,ETH-USD,USDT-USD,BNB-USD,SOL-USD,XRP-USD,DOGE-USD,SHIB-USD,XRP-USD");
        }

        private List<Asset> ConvertToAssetObjects(Root root)
        {
            List<Asset> assetQuotes = new List<Asset>();

            if (root != null && root.quoteResponse != null && root.quoteResponse.result != null)
            {
                foreach (var result in root.quoteResponse.result)
                {
                    Asset assetQuote = new Asset
                    {
                        Symbol = result.symbol,
                        DisplayName = result.displayName,
                        RegularMarketChange = result.regularMarketChange,
                        RegularMarketChangePercent = result.regularMarketChangePercent,
                        FiftyTwoWeekLow = result.fiftyTwoWeekLow,
                        FiftyTwoWeekHigh = result.fiftyTwoWeekHigh,
                        CoinImageUrl = result.coinImageUrl,
                        ShortName = result.shortName,
                        QuoteType = result.quoteType,
                        MarketState = result.marketState,
                        RegularMarketOpen = result.regularMarketOpen,
                        RegularMarketDayHigh = result.regularMarketDayHigh,
                        RegularMarketPrice = result.regularMarketPrice
                    };
                    assetQuotes.Add(assetQuote);
                }
            }

            return assetQuotes;
        }
    }
}
