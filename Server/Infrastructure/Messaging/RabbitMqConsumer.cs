using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using BankTransactionService.Model.Entities;
using BankTransactionService.Domain.Repositories;
using BankTransactionService.Infrastructure.Configuration;
namespace BankTransactionService.Infrastructure.Messaging
{
    public class RabbitMqConsumer
    {
        private readonly RabbitMqSettings _settings;
        private readonly IBankTransactionRepository _repository;
        private IConnection _connection;
        private IModel _channel;
        
        public RabbitMqConsumer(RabbitMqSettings settings, IBankTransactionRepository repository)
        {
            _settings = settings;
            _repository = repository;
            InitializeRabbitMq();
        }
        
        private void InitializeRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password
            };
            
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _settings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }
        
        public void Start()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var transaction = JsonConvert.DeserializeObject<BankTransactionEntity>(message);
                await _repository.AddAsync(transaction);
            };
            _channel.BasicConsume(queue: _settings.QueueName, autoAck: true, consumer: consumer);
        }
        
        public void Stop()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}