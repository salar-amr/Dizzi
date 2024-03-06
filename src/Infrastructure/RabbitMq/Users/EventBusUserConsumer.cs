using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;

namespace Infrastructure.RabbitMq.PlaceOrderGift;
public class EventBusUserConsumerRequest
{
    public int UserId { get; set; }

}

public class EventBusUserConsumer : BaseConsumer
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<EventBusUserConsumer> _logger;

    public EventBusUserConsumer(IRabbitMQConnection connection, IServiceProvider serviceProvider, ILogger<EventBusUserConsumer> logger) : base(connection)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
    {
        try
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            _logger.LogInformation($"EventBusPlaceOrderGiftConsumer = {message}");
            var messameModel = JsonSerializer.Deserialize<EventBusUserConsumerRequest>(message);
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ISender mediatRSender =
                    scope.ServiceProvider.GetRequiredService<ISender>();

                //await mediatRSender.Send(new CreateUserCommand
                //{
                //    UserId = messameModel.UserId,
                //});
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(eventId: new EventId(1), exception: ex, message: "EventBusPlaceOrderGiftConsumer error");
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() => { Consume(EventBusConstants.DIZZI_USER, ReceivedEvent); });
    }
}
