
using BankTransactionService.Application.DTO;

namespace BankTransactionService.Application.Interfaces
{
    public interface IBankTransactionService
    {
        Task AddBankTransactionAsync(BankTransactionDto transactionDto);
        Task<IEnumerable<BankTransactionDto>> GetBankTransactionsAsync();
        Task<BankTransactionDto> GetBankTransactionByIdAsync(string id);
    }
}
