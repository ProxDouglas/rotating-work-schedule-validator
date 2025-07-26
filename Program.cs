using WorkSchedule.Order.Validators;
using WorkSchedule.Swagger.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
// builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ValidateOrderSchedule>();
builder.Services.AddSwaggerGen(c =>
{
    // Configuração para usar descrições dos enums
    c.SchemaFilter<EnumSchemaFilter>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
