using Microsoft.AspNetCore.Cors.Infrastructure;
using Zamin.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddZaminInMemoryCaching();
builder.Services.AddZaminNewtonSoftSerializer();
builder.Services.AddZaminApiAuthentication(builder.Configuration, "ApiAuthentication");
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(delegate (CorsPolicyBuilder builder)
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();
app.Run();
