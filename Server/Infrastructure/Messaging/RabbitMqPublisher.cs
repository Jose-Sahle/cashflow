
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using BankTransactionService.Model.Entities;
using BankTransactionService.Infrastructure.Configuration;

namespace BankTransactionService.Infrastructure.Messaging
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMqSettings _settings;
        
        public RabbitMqPublisher(RabbitMqSettings settings)
        {
            _settings = settings;
        }
        
        public void Publish(BankTransactionEntity transaction)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _settings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(transaction));
            channel.BasicPublish(exchange: "", routingKey: _settings.QueueName, basicProperties: null, body: body);
        }
    }
}
