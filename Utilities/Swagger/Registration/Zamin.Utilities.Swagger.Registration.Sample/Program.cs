using Zamin.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddZaminApiAuthentication(builder.Configuration, "ApiAuthentication");
builder.Services.AddZaminSwagger(builder.Configuration, "Swagger");

var app = builder.Build();

app.UseZaminSwagger();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();