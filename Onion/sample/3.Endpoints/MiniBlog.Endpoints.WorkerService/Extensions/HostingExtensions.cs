using Microsoft.EntityFrameworkCore;
using MiniBlog.Endpoints.WorkerService.Extensions;
using MiniBlog.Endpoints.WorkerService.Workers;
using MiniBlog.Infra.Data.Sql.Commands.Common;
using MiniBlog.Infra.Data.Sql.Queries.Common;
using Zamin.Extensions.DependencyInjection;

namespace MiniBlog.Endpoints.WorkerService.Extensions;

public static class HostingExtensions
{
    public static IHost ConfigureServices(this IHostBuilder builder)
    {
        return builder.ConfigureServices(services =>
        {
            string cnn = "Server=.; Initial Catalog=MiniBlogDb; User Id=sa; Password=1qaz!QAZ";

            services.AddZaminParrotTranslator(c =>
            {
                c.ConnectionString = cnn;
                c.AutoCreateSqlTable = true;
                c.SchemaName = "dbo";
                c.TableName = "ParrotTranslations";
                c.ReloadDataIntervalInMinuts = 1;
            });

            services.AddZaminWebUserInfoService(true);

            services.AddZaminAutoMapperProfiles(option =>
            {
                option.AssmblyNamesForLoadProfiles = "Miniblog";
            });

            services.AddZaminMicrosoftSerializer();

            services.AddZaminInMemoryCaching();

            services.AddDbContext<MiniblogCommandDbContext>(c => c.UseSqlServer(cnn), ServiceLifetime.Singleton);
            services.AddDbContext<MiniblogQueryDbContext>(c => c.UseSqlServer(cnn), ServiceLifetime.Singleton);

            services.AddZaminDependenciesCustomized("Zamin", "MiniBlog");

            services.AddHostedService<WorkerHostedService>();
        }).Build();
    }
}