using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using Zamin.Infra.Tools.Caching.Microsoft;
using Zamin.Infra.Tools.OM.AutoMapper.DipendencyInjections;
using Zamin.EndPoints.Web.Configurations;
using Zamin.Utilities;
using Zamin.Utilities.Services.Chaching;
using Zamin.Utilities.Services.Logger;
using Zamin.Utilities.Services.Serializers;
using Zamin.Utilities.Services.Users;
using Zamin.Utilities.Services.Localizations;

namespace Zamin.EndPoints.Web.StartupExtentions
{
    public static class AddZaminServicesExtentions
    {
        public static IServiceCollection AddZaminServices(
            this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            services.AddCaching();
            services.AddLogging();
            services.AddJsonSerializer(assembliesForSearch);
            services.AddObjectMapper(assembliesForSearch);
            services.AddUserInfoService(assembliesForSearch);
            services.AddResourceManager(assembliesForSearch);
            services.AddTransient<ZaminServices>();
            return services;
        }
        private static IServiceCollection AddCaching(this IServiceCollection services)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if (_zaminConfigurations?.Chaching?.Enable == true)
            {
                if (_zaminConfigurations.Chaching.Provider == ChachProvider.MemoryCache)
                {
                    services.AddScoped<ICacheAdapter, InMemoryCacheAdapter>();
                }
                else
                {
                    services.AddScoped<ICacheAdapter, DistributedCacheAdapter>();
                }

                switch (_zaminConfigurations.Chaching.Provider)
                {
                    case ChachProvider.DistributedSqlServerCache:
                        services.AddDistributedSqlServerCache(options =>
                        {
                            options.ConnectionString = _zaminConfigurations.Chaching.DistributedSqlServerCache.ConnectionString;
                            options.SchemaName = _zaminConfigurations.Chaching.DistributedSqlServerCache.SchemaName;
                            options.TableName = _zaminConfigurations.Chaching.DistributedSqlServerCache.TableName;
                        });
                        break;
                    case ChachProvider.StackExchangeRedisCache:
                        services.AddStackExchangeRedisCache(options =>
                        {
                            options.Configuration = _zaminConfigurations.Chaching.StackExchangeRedisCache.Configuration;
                            options.InstanceName = _zaminConfigurations.Chaching.StackExchangeRedisCache.SampleInstance;
                        });
                        break;
                    case ChachProvider.NCacheDistributedCache:
                        throw new NotSupportedException("NCache Not Supporting yet");
                    default:
                        services.AddMemoryCache();
                        break;
                }
            }
            else
            {
                services.AddScoped<ICacheAdapter, NullObjectCacheAdapter>();
            }
            return services;
        }

        private static IServiceCollection AddLogging(this IServiceCollection services)
        {
            return services.AddScoped<IScopeInformation, ScopeInformation>();

        }

        public static IServiceCollection AddJsonSerializer(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.Where(type => type.Name == _zaminConfigurations.JsonSerializerTypeName && typeof(IJsonSerializer).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }



        private static IServiceCollection AddObjectMapper(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if (_zaminConfigurations.RegisterAutomapperProfiles)
            {
                services.AddAutoMapperProfiles(assembliesForSearch);
            }
            return services;
        }

        private static IServiceCollection AddUserInfoService(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _zaminConfigurations.UserInfoServiceTypeName && typeof(IUserInfoService).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }

        private static IServiceCollection AddResourceManager(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _zaminConfigurations.Translator.TranslatorTypeName && typeof(ITranslator).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }
    }
}
