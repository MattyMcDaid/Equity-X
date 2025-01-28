using EquityX_L00160463.Models;
using SQLite;

namespace EquityX_L00160463.Services
{
    public class LocalDbService
    {
        private const string DbName = "EquityXDb";
        private readonly SQLiteAsyncConnection _connection;


        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DbName));
            _connection.CreateTableAsync<BankInfo>();
        }


        //get all bank info
        public async Task<List<BankInfo>> GetBankInfo()
        {
            return await _connection.Table<BankInfo>().ToListAsync();
        }

        //get bank info by id
        public async Task<BankInfo> GetBankInfoById(int id)
        {
            return await _connection.Table<BankInfo>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //create bank info
        public async Task Create(BankInfo bankInfo)
        {
            await _connection.InsertAsync(bankInfo);
        }

        //update bank info

        public async Task Update(BankInfo bankInfo)
        {
            await _connection.UpdateAsync(bankInfo);
        }

        //delete bank info
        public async Task Delete(BankInfo bankInfo)
        {
            await _connection.DeleteAsync(bankInfo);
        }
    }


}
