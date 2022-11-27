using Microsoft.EntityFrameworkCore;
using OpenTelemetryRegistration.Extensions.DependencyInjection;
using OpenTelemetryRegistration.Sample.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PersonContext>(c => c.UseSqlServer("Server=10.100.8.173;User Id=sa;Password=2wsx@WSX; Initial catalog=PersonDbTrace;Encrypt=false"));
builder.Services.AddZaminTraceJeager(c =>
{
    c.AgentHost= "localhost";
    c.ApplicationName = "Zamin";
    c.ServiceName = "OpenTelemetrySample";
    c.ServiceVersion= "1.0.0";
    c.ServiceId = "cb387bb6-9a66-444f-92b2-ff64e2a81f98";
});
var app = builder.Build();

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
