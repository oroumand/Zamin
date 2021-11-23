using Microsoft.EntityFrameworkCore;
using Zamin.EndPoints.Web;
using Zamin.EndPoints.Web.StartupExtentions;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common;
using Zamin.MiniBlog.Infra.Data.Sql.Queries.Common;
using Zamin.Utilities.Configurations;

var builder = new ZaminProgram().Main(args, "appsettings.json", "appsettings.zamin.json", "appsettings.serilog.json");

//Configuration
ConfigurationManager Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddZaminApiServices(Configuration);
builder.Services.AddDbContext<MiniblogDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("MiniBlogCommand_ConnectionString")));
builder.Services.AddDbContext<MiniblogQueryDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("MiniBlogCommand_ConnectionString")));

//Middlewares
var app = builder.Build();
var zaminOptions = app.Services.GetService<ZaminConfigurationOptions>();

app.UseZaminApiConfigure(zaminOptions, app.Environment);

app.Run();