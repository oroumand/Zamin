using Zamin.Extensions.DependencyInjection;
using Zamin.Extensions.MessageBus.Abstractions;
using Zamin.Extensions.MessageBus.Abstractions.Fakes;
using Zamin.Extensions.MessageBus.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddZaminNewtonSoftSerializer();
builder.Services.AddZaminRabbitMqMessageBus(c =>
{
    c.PerssistMessage = true;
    c.ExchangeName = "SampleExchange";
    c.ServiceName = "SampleApplciatoin";
    c.Url = @"amqp://guest:guest@localhost:5672/";
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageConsumer, FakeMessageConsumer>();

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
