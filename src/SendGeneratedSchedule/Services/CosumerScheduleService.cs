namespace WorkSchedule.SendGeneratedSchedule.Services;

using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WorkSchedule.SendGeneratedSchedule.Entities;
using WorkSchedule.SendGeneratedSchedule.Send;

public class CosumerScheduleService : BackgroundService
{
   private readonly IConnection _connection;
   private IChannel _channel;
   private AsyncEventingBasicConsumer? _consumer;

   public CosumerScheduleService(IConnection connection)
   {
      _connection = connection;
   }

   protected override async Task ExecuteAsync(CancellationToken stoppingToken)
   {
      try
      {
         if (this._channel == null)
            this._channel = await _connection.CreateChannelAsync();

         // Configura o consumidor
         _consumer = new AsyncEventingBasicConsumer(_channel);
         _consumer.ReceivedAsync += async (model, ea) => this.ConsumeMessage(model, ea);

         // Inicia o consumo
         var consumerTag = await _channel.BasicConsumeAsync(
             queue: "GenerationResponse",
             autoAck: false,
             consumer: _consumer);
      }
      catch (OperationCanceledException)
      {
         Console.WriteLine("Timeout ou cancelamento durante o consumo da mensagem");
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Erro ao consumir mensagem: {ex.Message}");
      }
   }

   public async void ConsumeMessage(object model, BasicDeliverEventArgs ea)
   {
      try
      {
         WorkScheduleGenerated? message = deserializeObject(ea);
         if (message != null)
         {
            WorkScheduleSendFactory workScheduleSendFactory = new WorkScheduleSendFactory();
            IWorkScheduleGeneratedSend send = workScheduleSendFactory.Build(message);
         }
      }
      catch (Exception ex)
      {
         Console.WriteLine($"Erro ao consumir mensagem: {ex.Message}");
      }
      finally
      {
         // Confirma o recebimento da mensagem (ack)
         await _channel.BasicAckAsync(ea.DeliveryTag, false);
      }
   }

   private WorkScheduleGenerated? deserializeObject(BasicDeliverEventArgs result)
   {
      var message = System.Text.Encoding.UTF8.GetString(result.Body.ToArray());
      if (string.IsNullOrEmpty(message))
         return default;
      return System.Text.Json.JsonSerializer.Deserialize<WorkScheduleGenerated>(message);
   }
}
