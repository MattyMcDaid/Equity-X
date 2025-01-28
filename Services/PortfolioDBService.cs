
using EquityX_L00160463.Models;
using SQLite;

namespace EquityX_L00160463.Services
{
    public class PortfolioDBService
    {
        private SQLiteAsyncConnection db;


        public PortfolioDBService()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "PortfolioData.db");

            db = new SQLiteAsyncConnection(databasePath);

            db.CreateTableAsync<Portfolio>();
            db.InsertAsync(new Portfolio { TotalValue = 0, FundsAvailable = 0, ProfitOrLoss = 0, TotalInvested = 0 });
        }
        public async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "PortfolioData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Portfolio>();
            await db.InsertAsync(new Portfolio { TotalValue = 0, FundsAvailable = 0, ProfitOrLoss = 0, TotalInvested = 0 });
        }

        public async Task<Portfolio> GetPortfolioById(int id)
        {
            return await db.Table<Portfolio>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Portfolio portfolio)
        {
            await db.InsertAsync(portfolio);
        }

        public async Task Update(Portfolio portfolio)
        {
            await db.UpdateAsync(portfolio);
        }

        public async Task Delete(Portfolio portfolio)
        {
            await db.DeleteAsync(portfolio);
        }

        public async Task<double> GetFunds(int id)
        {
            var portfolio = await GetPortfolioById(id);

            if (portfolio == null)
            {
                throw new Exception("Portfolio not found.");
            }

            return portfolio.FundsAvailable;
        }

        public async Task DepositFunds(int id, double amount)
        {
            var portfolio = await GetPortfolioById(id);

            if (portfolio == null)
            {
                throw new Exception("Portfolio not found.");
            }

            portfolio.FundsAvailable += amount;
            portfolio.TotalValue += amount;
            await db.UpdateAsync(portfolio);

        }

        public async Task WithdrawFunds(int id, double amount)
        {
            var portfolio = await GetPortfolioById(id);
            if (portfolio == null)
            {
                throw new Exception("Portfolio not found.");
            }
            if (portfolio.FundsAvailable >= amount)
            {
                portfolio.FundsAvailable -= amount;
                portfolio.TotalValue -= amount;
                await db.UpdateAsync(portfolio);
            }
            else
            {
                throw new Exception("Insufficient funds.");
            }
        }

    }
}