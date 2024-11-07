namespace BankTransactionService.Model.Entities
{
    public class BankTransactionEntity : BaseEntity
    {
        public enum BankTransactionType { Credit, Debit }
        public decimal Amount { get; protected set; }
        public DateTime Date { get; protected set; }
        public string Description { get; protected set; }
        public BankTransactionType Type { get; protected set; }
        
        public BankTransactionEntity(string description, decimal amount, DateTime date, BankTransactionType type) : base()
        {
            Description = description;
            Amount = amount;
            Date = date;
            Type = type;
        }
    }

}
