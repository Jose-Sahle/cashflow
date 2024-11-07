
using MongoDB.Driver;
using BankTransactionService.Model.Entities;
using BankTransactionService.Domain.Repositories;
using BankTransactionService.Infrastructure.Configuration;

namespace BankTransactionService.Infrastructure.Repositories
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        private readonly IMongoCollection<BankTransactionEntity> _bankTransactions;
        
        public BankTransactionRepository(MongoDbContext context)
        {
            _bankTransactions = context.BankTransactions;
        }
        
        public async Task<BankTransactionEntity> GetByIdAsync(string id)
        {
            return await _bankTransactions.Find(transaction => transaction.Id == id).FirstOrDefaultAsync();
        }
        
        public async Task<IEnumerable<BankTransactionEntity>> GetAllAsync()
        {
            return await _bankTransactions.Find(transaction => true).ToListAsync();
        }
        
        public async Task AddAsync(BankTransactionEntity transaction)
        {
            await _bankTransactions.InsertOneAsync(transaction);
        }
    }
}
