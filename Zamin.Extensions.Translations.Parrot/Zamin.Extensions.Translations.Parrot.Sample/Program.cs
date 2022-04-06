using Zamin.Extensions.Translations.Parrot.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// برای فعال سازی مترجم باید تنظیمات مرتبط با دیتابیس و محدوده زمانی که داده‌ها از دیتابیس بارگذاری مجدد می‌شود را تعیین کنیم.
/// </summary>
builder.Services.AddParrotTranslator(c =>
{
    c.ConnectionString = "Server=.; Initial Catalog=MyTranslatorDb; User Id=sa; Password=1qaz!QAZ";
    c.AutoCreateSqlTable = true;
    c.SchemaName = "dbo";
    c.TableName = "Motarjem";
    c.ReloadDataIntervalInMinuts = 1;
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
