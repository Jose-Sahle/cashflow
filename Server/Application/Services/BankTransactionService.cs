
using BankTransactionService.Application.DTO;
using BankTransactionService.Application.Interfaces;
using BankTransactionService.Model.Entities;
using BankTransactionService.Domain.Exceptions;
using BankTransactionService.Domain.Repositories;
using BankTransactionService.Infrastructure.Messaging;

namespace BankTransactionService.Application.Services
{
    public class BankTransactionService : IBankTransactionService
    {
        private readonly IBankTransactionRepository _repository;
        private readonly RabbitMqPublisher _publisher;
        public BankTransactionService(IBankTransactionRepository repository, RabbitMqPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }
        public async Task AddBankTransactionAsync(BankTransactionDto transactionDto)
        {
            var transaction = new BankTransactionEntity(
                transactionDto.Description,
                transactionDto.Amount,
                transactionDto.Date,
                transactionDto.Type
            );
            _publisher.Publish(transaction);
        }
        public async Task<IEnumerable<BankTransactionDto>> GetBankTransactionsAsync()
        {
            var transactions = await _repository.GetAllAsync();
            return transactions.Select(t => new BankTransactionDto
            {
                Description = t.Description,
                Amount = t.Amount,
                Date = t.Date,
                Type = t.Type
            });
        }
        public async Task<BankTransactionDto> GetBankTransactionByIdAsync(string id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null)
            {
                throw new TransactionNotFoundException(id);
            }
            return new BankTransactionDto
            {
                Description = transaction.Description,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type
            };
        }
    }
}
