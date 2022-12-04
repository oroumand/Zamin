using Zamin.Extensions.DependencyInjection;

#region Services
var builder = WebApplication.CreateBuilder(args);

//microsoft
builder.Services.AddControllers();

//zamin
builder.Services.AddZaminNewtonSoftSerializer();

//zamin RabbitMqMessageBus
builder.Services.AddZaminRabbitMqMessageBus(c =>
{
    c.PerssistMessage = true;
    c.ExchangeName = "ZamminRabbitMqExchange";
    c.ServiceName = "ZamminRabbitMqService";
    c.Url = @"amqp://guest:guest@localhost:5672/";
});

//microsoft
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();
#endregion

#region Pipeline
app.Services.ReceiveCommandFromRabbitMqMessageBus("PersonCommand");

app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("SampleApplciatoin", "PersonEvent"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(); 
#endregion