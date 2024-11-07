using BankTransactionService.Model.Entities;

namespace BankTransactionService.Domain.Services
{
    public interface IBankTransactionService
    {
        Task AddBankTransactionAsync(string description, decimal amount, DateTime date, BankTransactionEntity.BankTransactionType type);
        Task<IEnumerable<BankTransactionEntity>> GetBankTransactionsAsync();
        Task<BankTransactionEntity> GetBankTransactionByIdAsync(Guid id);
    }
}
