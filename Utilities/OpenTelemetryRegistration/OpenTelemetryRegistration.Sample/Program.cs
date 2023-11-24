using Microsoft.EntityFrameworkCore;
using Zamin.Extensions.DependencyInjection;
using Zamin.Utilities.OpenTelemetryRegistration.Sample.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PersonContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddZaminTraceSupport(c =>
{
    c.ApplicationName = "Zamin";
    c.ServiceName = "OpenTelemetrySample";
    c.ServiceVersion = "1.0.0";
    c.ServiceId = "cb387bb6-9a66-444f-92b2-ff64e2a81f98";
    c.OltpEndpoint = "http://localhost:4317";
    c.ExportProcessorType = OpenTelemetry.ExportProcessorType.Simple; 
    c.SamplingProbability = 1;
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

SeedData.EnsurePopulate(app);

app.Run();
