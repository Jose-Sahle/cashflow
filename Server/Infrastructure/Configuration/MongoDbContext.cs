
using MongoDB.Driver;
using BankTransactionService.Model.Entities;

namespace BankTransactionService.Infrastructure.Configuration
{
    /*public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }
        public IMongoCollection<BankTransactionEntity> BankTransactions => _database.GetCollection<BankTransactionEntity>("BankTransaction");
    }*/

    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        
        public MongoDbContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
            EnsureDatabaseAndCollectionCreated<BankTransactionEntity>("BankTransactions");
        }
        
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        public IMongoCollection<BankTransactionEntity> BankTransactions => _database.GetCollection<BankTransactionEntity>("BankTransactions");
        
        private void EnsureDatabaseAndCollectionCreated<T>(string collectionName)
        {
            var collectionNames = _database.ListCollectionNames().ToList();
            if (!collectionNames.Contains(collectionName))
            {
                _database.CreateCollection(collectionName);
            }
        }
    }
}