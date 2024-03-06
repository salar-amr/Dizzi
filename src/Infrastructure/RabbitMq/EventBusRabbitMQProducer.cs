using System.Text;
using RabbitMQ.Client;

namespace Infrastructure.RabbitMq;
public class EventBusRabbitMQProducer
{
    private readonly IRabbitMQConnection _connection;

    public EventBusRabbitMQProducer(IRabbitMQConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public void Publish<T>(string queueName, T publishModel)
    {
        using (var channel = _connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            var message = System.Text.Json.JsonSerializer.Serialize(publishModel);
            var body = Encoding.UTF8.GetBytes(message);

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.DeliveryMode = 2;

            channel.ConfirmSelect();
            channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true, basicProperties: properties, body: body);
            channel.WaitForConfirmsOrDie();

            channel.BasicAcks += (sender, eventArgs) =>
            {
                // Console.WriteLine("Sent RabbitMQ");
                //implement ack handle
            };
            channel.ConfirmSelect();
        }
    }
}
