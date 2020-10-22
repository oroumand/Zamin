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
using Zamin.Utilities.Services.Messaging;
using Zamin.Infra.Events.Outbox;

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
            services.AddAsyncMessagePublisher(assembliesForSearch);
            services.AddOutBox(assembliesForSearch);
            services.AddTransient<ZaminServices>();
            return services;
        }
        
        private static IServiceCollection AddCaching(this IServiceCollection services)
        {
            var _ZaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if (_ZaminConfigurations?.Chaching?.Enable == true)
            {
                if (_ZaminConfigurations.Chaching.Provider == ChachProvider.MemoryCache)
                {
                    services.AddScoped<ICacheAdapter, InMemoryCacheAdapter>();
                }
                else
                {
                    services.AddScoped<ICacheAdapter, DistributedCacheAdapter>();
                }

                switch (_ZaminConfigurations.Chaching.Provider)
                {
                    case ChachProvider.DistributedSqlServerCache:
                        services.AddDistributedSqlServerCache(options =>
                        {
                            options.ConnectionString = _ZaminConfigurations.Chaching.DistributedSqlServerCache.ConnectionString;
                            options.SchemaName = _ZaminConfigurations.Chaching.DistributedSqlServerCache.SchemaName;
                            options.TableName = _ZaminConfigurations.Chaching.DistributedSqlServerCache.TableName;
                        });
                        break;
                    case ChachProvider.StackExchangeRedisCache:
                        services.AddStackExchangeRedisCache(options =>
                        {
                            options.Configuration = _ZaminConfigurations.Chaching.StackExchangeRedisCache.Configuration;
                            options.InstanceName = _ZaminConfigurations.Chaching.StackExchangeRedisCache.SampleInstance;
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
            var _ZaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.Where(type => type.Name == _ZaminConfigurations.JsonSerializerTypeName && typeof(IJsonSerializer).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }

        private static IServiceCollection AddObjectMapper(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var _ZaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if (_ZaminConfigurations.RegisterAutomapperProfiles)
            {
                services.AddAutoMapperProfiles(assembliesForSearch);
            }
            return services;
        }
        private static IServiceCollection AddUserInfoService(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _ZaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _ZaminConfigurations.UserInfoServiceTypeName && typeof(IUserInfoService).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
        private static IServiceCollection AddResourceManager(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _ZaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _ZaminConfigurations.Translator.TranslatorTypeName && typeof(ITranslator).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }

        public static IServiceCollection AddAsyncMessagePublisher(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var _ZaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.Where(type => type.Name == _ZaminConfigurations.Messaging.MessageBusTypeName && typeof(IAsyncMessagePublisher).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }


        public static IServiceCollection AddOutBox(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var _ZaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if(_ZaminConfigurations?.Messaging?.EventOutbox?.Enabled == true)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(c => c.Where(type => type.Name == _ZaminConfigurations.Messaging.EventOutbox.OutBoxRepositoryTypeName && typeof(IOutBoxEventItemRepository).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
                services.AddHostedService<OutboxEventPublisher>();
            }
            return services;
        }
    }
}
