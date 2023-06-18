using Microsoft.EntityFrameworkCore;
using Zamin.Extensions.ChangeDataLog.Hamster.Sample.DAL;
using Zamin.Extensions.DependencyInjection;
using Zamin.Infra.Data.Sql.Commands.Interceptors;

string cnnString = "Server=;User Id=;Database=; Password=;MultipleActiveResultSets=true;Encrypt = false";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddZaminChageDatalogDalSql(c =>
{
    c.ConnectionString = cnnString;

});
builder.Services.AddZaminHamsterChageDatalog(c =>
{
    c.BusinessIdFieldName= "Id";
});
builder.Services.AddZaminWebUserInfoService(c =>
{
    c.DefaultUserId = "1";
},useFake:true);
builder.Services.AddDbContext<HamsterTestContext>(c => c.UseSqlServer(cnnString).AddInterceptors(new AddChangeDataLogInterceptor()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
