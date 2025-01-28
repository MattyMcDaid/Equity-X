using EquityX_L00160463.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityX_L00160463.Services
{
    public interface IAssetDataService
    {
        Task<List<Asset>> GetAssetsData();

        Task<List<Asset>> GetFeaturedAssetsData();


    }
}