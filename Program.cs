using WorkSchedule.Filters;
using WorkSchedule.AplicationStartup.RabbitMq;
using WorkSchedule.GenerationRequest.Validators;
using WorkSchedule.GenerationRequest.Mapper;
using WorkSchedule.SendGeneratedSchedule.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
// builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ValidateOrderSchedule>();
builder.Services.AddScoped<WorkScheduleRequestMapper>();
builder.Services.AddSwaggerGen(c =>
{
    // Configuração para usar descrições dos enums
    c.SchemaFilter<EnumSchemaFilter>();
});

// Configurar RabbitMQ
new RabbitMqStartUp().ConfigureServices(builder);

builder.Services.AddHostedService<CosumerScheduleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
