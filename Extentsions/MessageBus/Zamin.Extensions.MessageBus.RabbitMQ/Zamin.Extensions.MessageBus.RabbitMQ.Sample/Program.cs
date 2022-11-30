using Zamin.Extensions.DependencyInjection;
using Zamin.Extensions.MessageBus.RabbitMQ.Sample.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddZaminNewtonSoftSerializer();
builder.Services.AddZaminRabbitMqMessageBus(c =>
{
    c.PerssistMessage = true;
    c.ExchangeName = "SampleExchange";
    c.ServiceName = "SampleApplciatoin";
    c.Url = @"amqp://guest:guest@localhost:5672/";
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("SampleApplciatoin", "PersonEvent"));
app.Services.ReceiveCommandFromRabbitMqMessageBus("PersonCommand");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
