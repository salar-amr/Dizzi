using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.RabbitMq;
public abstract class BaseConsumer : BackgroundService
{
    private readonly IRabbitMQConnection _connection;

    public BaseConsumer(IRabbitMQConnection connection)
    {
        _connection = connection;
    }

    public void Consume(string queueName, EventHandler<BasicDeliverEventArgs> receivedEvent)
    {
        var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += receivedEvent;

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }

}
