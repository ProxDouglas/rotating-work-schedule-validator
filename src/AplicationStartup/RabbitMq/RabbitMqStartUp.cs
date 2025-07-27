namespace WorkSchedule.AplicationStartup.RabbitMq;

using RabbitMQ.Client;
using WorkSchedule.QueueRabbitMQ;

public class RabbitMqStartUp
{
  public async Task<IConnection> CreateConnectionAsync(WebApplicationBuilder builder)
  {
    var factory = new ConnectionFactory();

    // Usa as configurações do appsettings.json
    factory.HostName = builder.Configuration["RabbitMQ:Host"] ?? "localhost";
    factory.Port = int.Parse(builder.Configuration["RabbitMQ:Port"] ?? "5672");
    factory.UserName = builder.Configuration["RabbitMQ:Username"] ?? "guest";
    factory.Password = builder.Configuration["RabbitMQ:Password"] ?? "guest";
    factory.VirtualHost = builder.Configuration["RabbitMQ:VirtualHost"] ?? "/";

    try
    {
      return await factory.CreateConnectionAsync();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Erro ao conectar com RabbitMQ: {ex.Message}");
      throw;
    }
  }

  public void ConfigureServices(WebApplicationBuilder builder)
  {
    // Registra o IConnection como um singleton
    var connection = CreateConnectionAsync(builder).GetAwaiter().GetResult();
    builder.Services.AddSingleton<IConnection>(connection);

    // Registra o QueueProducer
    builder.Services.AddScoped<IQueuePubSub, QueuePubSub>();
    try
    {
      builder.Services.Configure<RabbitMqConfig>(
              builder.Configuration.GetSection("RabbitMQ"));

      builder.Services.Configure<RabbitMqConfig>("Schedule",
          builder.Configuration.GetSection("RabbitMQ_Schedule"));

      // Registra a conexão como singleton
      builder.Services.AddSingleton<IConnection>(serviceProvider =>
      {
        return CreateConnectionAsync(builder).GetAwaiter().GetResult();
      });
      Console.WriteLine("RabbitMQ configurado com sucesso");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Erro na configuração do RabbitMQ: {ex.Message}");
      throw;
    }
    // Registra a configuração do RabbitMQ
  }
}

public class RabbitMqConfig
{
  public string Host { get; set; } = "localhost";
  public int Port { get; set; } = 5672;
  public string Username { get; set; } = "guest";
  public string Password { get; set; } = "guest";
  public string VirtualHost { get; set; } = "/";
  public string Queue { get; set; } = string.Empty;
}
