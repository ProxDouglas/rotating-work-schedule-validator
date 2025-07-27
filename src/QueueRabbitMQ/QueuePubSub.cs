namespace WorkSchedule.QueueRabbitMQ;

using RabbitMQ.Client;

public class QueuePubSub : IQueuePubSub
{
   private readonly IConnection _connection;
   private readonly IChannel channel;

   public QueuePubSub(IConnection connection)
   {
      _connection = connection;
      var channelTask = connection.CreateChannelAsync();
      channelTask.Wait();
      this.channel = channelTask.Result;
   }

   public async Task<bool> ProduceMessage<T>(T obj, string queueName, string exchange)
   {
      try
      {
         // Serializa o objeto para JSON
         var body = this.serializeObject(obj);

         // Publica a mensagem na fila
         await this.channel.BasicPublishAsync(
             exchange: exchange,
             routingKey: queueName,
             body: body,
             mandatory: true);

         return true;
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Erro ao enviar mensagem: {ex.Message}");
         return false;
      }
   }

   public async Task<T?> ConsumeMessage<T>(string queueName)
   {
      try
      {
         using var channel = await _connection.CreateChannelAsync();

         var result = await channel.BasicGetAsync(queue: queueName, autoAck: true);

         if (result == null || result.Body.IsEmpty)
            return default;

         return this.deserializeObject<T>(result);
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Erro ao consumir mensagem: {ex.Message}");
         return default;
      }
   }

   private byte[] serializeObject<T>(T obj)
   {
      var message = System.Text.Json.JsonSerializer.Serialize(obj);
      var body = System.Text.Encoding.UTF8.GetBytes(message);
      return body;
   }

   private T? deserializeObject<T>(BasicGetResult result)
   {
      var message = System.Text.Encoding.UTF8.GetString(result.Body.ToArray());
      if (string.IsNullOrEmpty(message))
         return default;
      return System.Text.Json.JsonSerializer.Deserialize<T>(message);
   }
}
