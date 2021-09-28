using Zamin.EndPoints.Web.StartupExtentions;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common;
using Zamin.MiniBlog.Infra.Data.Sql.Queries.Common;
using Zamin.Utilities.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zamin.MiniBlog.EndPoints.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddZaminApiServices(configuration);
            services.AddDbContext<MiniblogDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("MiniBlogCommand_ConnectionString")));
            services.AddDbContext<MiniblogQueryDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("MiniBlogCommand_ConnectionString")));

            

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ZaminConfigurationOptions zaminConfigurations)
        {
            app.UseZaminApiConfigure(zaminConfigurations, env);
        }
    }
}
