using Zamin.Extensions.DependencyInjection;
using Zamin.Utilities.SoftwarePartDetector.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient();

builder.Services.AddSoftwarePartDetector(config =>
{
   config.ApplicationName = "SampleApplicationName";
   config.ModuleName = "SampleModuleName";
   config.ServiceName = "SampleServiceName";
   config.DestinationServiceBaseAddress = "https://sample.com/";
   config.DestinationServicePath = "api/SoftwarePart/Create";
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Services.GetService<SoftwarePartDetectorService>()?.Run();

app.Run();
