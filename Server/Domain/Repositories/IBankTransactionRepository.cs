using BankTransactionService.Model.Entities;

namespace BankTransactionService.Domain.Repositories
{
    public interface IBankTransactionRepository
    {
        Task<BankTransactionEntity> GetByIdAsync(string id);
        Task<IEnumerable<BankTransactionEntity>> GetAllAsync();
        Task AddAsync(BankTransactionEntity transaction);
    }
}
