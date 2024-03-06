//using Application;
//using Application.Contracts;

//namespace Infrastructure.RabbitMq;

//public class MTQueueProducer : IMTQueueProducer
//{
//    private readonly EventBusRabbitMQProducer _eventBus;

//    public MTQueueProducer(EventBusRabbitMQProducer eventBus)
//    {
//        _eventBus = eventBus;
//    }

//    public async Task PublishDizziInAppMessageConsumer(BewMessageUser message)
//    {
//        _eventBus.Publish(EventBusConstants.DIZZI_USER, message);

//    }
//}
