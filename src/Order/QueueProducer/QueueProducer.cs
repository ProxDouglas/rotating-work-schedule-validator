namespace WorkSchedule.Order.QueueProducer;

using RabbitMQ.Client;

public class QueueProducer : IQueueProducer
{
   private readonly IConnection _connection;

   public QueueProducer(IConnection connection)
   {
      _connection = connection;
   }

   public async Task<bool> SendMessage<T>(T obj, string queueName, string exchange)
   {
      try
      {
         using var channel = await _connection.CreateChannelAsync();

         // Declara a fila se não existir
         await channel.QueueDeclareAsync(
             queue: queueName,
             durable: true,
             exclusive: false,
             autoDelete: false,
             arguments: null);

         // Serializa o objeto para JSON
         var message = System.Text.Json.JsonSerializer.Serialize(obj);
         var body = System.Text.Encoding.UTF8.GetBytes(message);

         // Publica a mensagem na fila
         await channel.BasicPublishAsync(
             exchange: exchange,
             routingKey: queueName,
             body: body);

         return true;
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
         return false;
      }
   }
}
