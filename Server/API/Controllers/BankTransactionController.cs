
using Microsoft.AspNetCore.Mvc;
using BankTransactionService.Application.DTO;
using BankTransactionService.Application.Interfaces;
using BankTransactionService.Model.Entities;

namespace TransactionService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankTransactionController : ControllerBase
    {
        private readonly IBankTransactionService _bankTransactionService;
        
        public BankTransactionController(IBankTransactionService bankTransactionService)
        {
            _bankTransactionService = bankTransactionService;
        }
        
        [HttpPost]
        [Route("AddBankTransaction/")]
        public async Task<IActionResult> AddBankTransaction([FromBody] BankTransactionDto request)
        {
            var transactionDto = new BankTransactionDto
            {
                Description = request.Description,
                Amount = request.Amount,
                Date = request.Date,
                Type = request.Type
            };
            await _bankTransactionService.AddBankTransactionAsync(transactionDto);
            return Ok();
        }
        
        [HttpGet]
        [Route("GetBankTransactions/")]
        public async Task<IActionResult> GetBankTransactions()
        {
            var transactions = await _bankTransactionService.GetBankTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("GetTransactionById/{id}")]
        public async Task<IActionResult> GetTransactionById(string id)
        {
            var transaction = await _bankTransactionService.GetBankTransactionByIdAsync(id);
            return Ok(transaction);
        }
    }
}
