using Zamin.Extensions.DependencyInjection;
using Zamin.Extensions.MessageBus.MessageInbox.Extensions.DependencyInjection;

#region Services
var builder = WebApplication.CreateBuilder(args);

//microsoft
builder.Services.AddControllers();

//microsoft
builder.Services.AddEndpointsApiExplorer();

//zamin
builder.Services.AddZaminNewtonSoftSerializer();

//zamin RabbitMq
builder.Services.AddZaminRabbitMqMessageBus(c =>
{
    c.PerssistMessage = true;
    c.ExchangeName = "MiniBlogExchange";
    c.ServiceName = "MiniBlog";
    c.Url = @"amqp://guest:guest@localhost:9672/";
});

//zamin MessageInbox
builder.Services.AddZaminMessageInbox(c =>
{
    c.ApplicationName = "MiniBlog";
    c.ConnectionString = "Server=.;Initial Catalog=MiniBlogDb;User Id=sa; Password=1qaz!QAZ;Encrypt=false";
    c.AutoCreateSqlTable = true;
    c.TableName = "MessageInbox";
    c.SchemaName = "dbo";
});
builder.Services.AddZaminSqlMessageInboxItemRepository();

builder.Services.AddSwaggerGen();

var app = builder.Build();
#endregion

#region Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Services.ReceiveEventFromRabbitMqMessageBus(new KeyValuePair<string, string>("MiniBlog", "BlogCreated"));

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion