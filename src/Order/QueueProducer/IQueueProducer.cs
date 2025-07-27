namespace WorkSchedule.Order.QueueProducer;

public interface IQueueProducer
{
    Task<bool> SendMessage<T>(T obj, string queueName, string exchange);
}
