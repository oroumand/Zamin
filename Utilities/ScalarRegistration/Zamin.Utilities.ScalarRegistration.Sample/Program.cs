using Zamin.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddZaminScalar(builder.Configuration, "Scalar");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseZaminScalar();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
