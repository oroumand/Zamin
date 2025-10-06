using Zamin.Extensions.DependencyInjection;
using Zamin.Extensions.ObjectMappers.Mapster.Sample.MapsterConfigs;
using Zamin.Extensions.ObjectMappers.Mapster.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
// رجیستر DI پکیج Mapster Adapter
builder.Services.AddMapsterMappings(typeof(Program).Assembly);

var app = builder.Build();
// رجیستر مپ‌ها (یکبار در ابتدا)
using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<IObjectMapper>();
    MapsterConfig.RegisterMappings(mapper);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();