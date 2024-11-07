using BankTransactionService.Infrastructure.Configuration;
using BankTransactionService.Infrastructure.Messaging;
using BankTransactionService.Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
namespace BankTransactionService.API.Services
{
    public class RabbitMqConsumerService : BackgroundService
    {
        private readonly ILogger<RabbitMqConsumerService> _logger;
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private RabbitMqConsumer _consumer;
        public RabbitMqConsumerService(ILogger<RabbitMqConsumerService> logger, IOptions<RabbitMqSettings> rabbitMqSettings, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _rabbitMqSettings = rabbitMqSettings.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
                _logger.LogInformation("RabbitMqConsumerService is stopping."));
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IBankTransactionRepository>();
                _consumer = new RabbitMqConsumer(_rabbitMqSettings, repository);
                _consumer.Start();
            }
            return Task.CompletedTask;
        }
        public override void Dispose()
        {
            _consumer.Stop();
            base.Dispose();
        }
    }
}