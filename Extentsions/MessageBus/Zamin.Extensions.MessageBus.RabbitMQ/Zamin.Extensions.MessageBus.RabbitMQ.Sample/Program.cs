using Zamin.Extensions.DependencyInjection;

WebApplication.CreateBuilder(args).Services.AddControllers();
WebApplication.CreateBuilder(args).Services.AddZaminNewtonSoftSerializer();
WebApplication.CreateBuilder(args).Services.AddZaminRabbitMqMessageBus(c =>
{
    c.PerssistMessage = true;
    c.ExchangeName = "SampleExchange";
    c.ServiceName = "SampleApplciatoin";
    c.Url = @"amqp://guest:guest@localhost:5672/";
});
WebApplication.CreateBuilder(args).Services.AddEndpointsApiExplorer();
WebApplication.CreateBuilder(args).Services.AddSwaggerGen();

var app = WebApplication.CreateBuilder(args).Build();
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
