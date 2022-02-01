using Microsoft.EntityFrameworkCore;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using MiniBlog.Infra.Data.Sql.Queries.Common;
using Zamin.EndPoints.Web;
using Zamin.EndPoints.Web.StartupExtentions;
using Zamin.Infra.Auth.AppPartsServices.ASPServices;
using Zamin.Infra.Auth.ControllerDetectors.Data;
using Zamin.Utilities.Configurations;

var builder = new ZaminProgram().Main(args, "appsettings.json", "appsettings.zamin.json", "appsettings.serilog.json");

//Configuration
ConfigurationManager Configuration = builder.Configuration;
builder.Services.AddZaminApiServices(Configuration);
builder.Services.AddDbContext<MiniblogCommandDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("MiniBlogCommand_ConnectionString")));
builder.Services.AddDbContext<MiniblogQueryDbContext>(c => c.UseSqlServer(Configuration.GetConnectionString("MiniBlogQuery_ConnectionString")));

//Middlewares
var app = builder.Build();
var zaminOptions = app.Services.GetRequiredService<ZaminConfigurationOptions>();

app.UseZaminApiConfigure(zaminOptions, app.Environment);

app.Run();