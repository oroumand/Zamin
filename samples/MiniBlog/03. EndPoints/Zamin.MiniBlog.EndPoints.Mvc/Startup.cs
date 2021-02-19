using Zamin.EndPoints.Web.StartupExtentions;
using Zamin.MiniBlog.Infra.Data.Sql.Commands.Common;
using Zamin.MiniBlog.Infra.Data.Sql.Queries.Common;
using Zamin.Utilities.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Zamin.MiniBlog.EndPoints.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddZaminMvcServices(_configuration);
            services.AddDbContext<MiniblogDbContext>(c => c.UseSqlServer(_configuration.GetConnectionString("MiniBlogCommand_ConnectionString")));
            services.AddDbContext<MiniblogQueryDbContext>(c => c.UseSqlServer(_configuration.GetConnectionString("MiniBlogCommand_ConnectionString")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ZaminConfigurations hamoonConfigurations)
        {
            app.UseZaminMvcConfigure(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }, hamoonConfigurations, env);
        }

    }
}
