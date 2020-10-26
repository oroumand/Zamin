using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zamin.EndPoints.Web.StartupExtentions;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common;
using Zamin.Utilities.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ZaminConfigurations zaminConfigurations)
        {
            app.UseZaminApiConfigure(zaminConfigurations, env);
        }
    }
}
