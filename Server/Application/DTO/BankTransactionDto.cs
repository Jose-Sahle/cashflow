
using static BankTransactionService.Model.Entities.BankTransactionEntity;

namespace BankTransactionService.Application.DTO
{
    public class BankTransactionDto
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public BankTransactionType Type { get; set; }
    }
}
