using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Zamin.EndPoints.Web;
using Zamin.EndPoints.Web.StartupExtentions;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common;
using Zamin.MiniBlog.Infra.Data.Sql.Queries.Common;
using Zamin.Utilities.Configurations;

var builder = new ZaminProgram().Main(args, "appsettings.json", "appsettings.hamoon.json", "appsettings.serilog.json");


//Configuration
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddZaminMvcServices(Configuration);
builder.Services.AddDbContext<MiniblogDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("MiniBlogCommand_ConnectionString")));
builder.Services.AddDbContext<MiniblogQueryDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("MiniBlogCommand_ConnectionString")));


//Middlewares
var app = builder.Build();
var zaminOptions = app.Services.GetService<ZaminConfigurationOptions>();


app.UseZaminMvcConfigure(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}, zaminOptions, app.Environment);

app.Run();