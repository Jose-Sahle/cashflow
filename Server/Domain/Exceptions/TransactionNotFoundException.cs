
namespace BankTransactionService.Domain.Exceptions
{
    public class TransactionNotFoundException : DomainException
    {
        public TransactionNotFoundException(string id) : base($"Transaction with ID {id} was not found.")
        {
            
        }
    }
}
