using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using WorkSchedule.AplicationStartup.RabbitMq;

namespace WorkSchedule.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RabbitMqTestController : ControllerBase
{
    private readonly IConnection _connection;
    private readonly RabbitMqConfig _rabbitMqConfig;

    public RabbitMqTestController(IConnection connection, IOptions<RabbitMqConfig> rabbitMqConfig)
    {
        _connection = connection;
        _rabbitMqConfig = rabbitMqConfig.Value;
    }

    [HttpPost("test-connection")]
    public async Task<IActionResult> TestConnection()
    {
        try
        {
            using var channel = await _connection.CreateChannelAsync();
            
            // Declara a fila se não existir
            await channel.QueueDeclareAsync(
                queue: _rabbitMqConfig.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            return Ok(new { 
                Message = "Conexão com RabbitMQ funcionando!",
                Queue = _rabbitMqConfig.Queue,
                Host = _rabbitMqConfig.Host,
                Port = _rabbitMqConfig.Port
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { 
                Error = "Erro na conexão com RabbitMQ", 
                Details = ex.Message 
            });
        }
    }

    [HttpPost("send-message")]
    public async Task<IActionResult> SendMessage([FromBody] object message)
    {
        try
        {
            using var channel = await _connection.CreateChannelAsync();
            
            // Declara a fila se não existir
            await channel.QueueDeclareAsync(
                queue: _rabbitMqConfig.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: _rabbitMqConfig.Queue,
                body: messageBody);

            return Ok(new { 
                Message = "Mensagem enviada com sucesso!",
                Queue = _rabbitMqConfig.Queue
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { 
                Error = "Erro ao enviar mensagem", 
                Details = ex.Message 
            });
        }
    }
}
