namespace BankTransactionService.Model.Entities
{
    public class BaseEntity
    {
        public string Id { get; private set; }

        public BaseEntity()
        { 
            Id = Guid.NewGuid().ToString();
        }
    }
}
