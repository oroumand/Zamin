using Zamin.Extensions.DependencyInjection;
using Zamin.Utilities.SoftwarePartDetector.Options;
using Zamin.Utilities.SoftwarePartDetector.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSoftwarePartDetector(config =>
{
    config.FakeSSL = true;
    config.ApplicationName = "SampleApplicationName";
    config.ModuleName = "SampleModuleName";
    config.ServiceName = "SampleServiceName";
    config.DestinationServiceBaseAddress = "https://sample.com/";
    config.DestinationServicePath = "api/SoftwarePart/Create";
    config.OAuth = new OAuthOption
    {
        Enabled = true,
        ClientId = "client",
        ClientSecret = "secret",
        Scopes = new[] { "softwareManagementApi" },
        Authority = "https://sample.com/",
    };
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
