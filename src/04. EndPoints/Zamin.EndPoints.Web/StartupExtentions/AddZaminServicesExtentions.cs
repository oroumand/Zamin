using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using Zamin.Infra.Tools.Caching.Microsoft;
using Zamin.Infra.Tools.OM.AutoMapper.DipendencyInjections;
using Zamin.Utilities;
using Zamin.Utilities.Services.Chaching;
using Zamin.Utilities.Services.Logger;
using Zamin.Utilities.Services.Serializers;
using Zamin.Utilities.Services.Users;
using Zamin.Utilities.Services.Localizations;
using Zamin.Utilities.Configurations;
using Zamin.Utilities.Services.MessageBus;
using Zamin.Infra.Events.Outbox;
using Zamin.Infra.Events.PoolingPublisher;
using Zamin.Messaging.IdempotentConsumers;
using Zamin.Infra.Data.ChangeInterceptors.EntityChageInterceptorItems;

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
            services.AddTranslator(assembliesForSearch);
            services.AddMessageBus(assembliesForSearch);
            services.AddPoolingPublisher(assembliesForSearch);
            services.AddTransient<ZaminServices>();
            services.AddEntityChangeInterception(assembliesForSearch);
            return services;
        }

        private static IServiceCollection AddCaching(this IServiceCollection services)
        {
            var _hamoonConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if (_hamoonConfigurations?.Chaching?.Enable == true)
            {
                if (_hamoonConfigurations.Chaching.Provider == ChachProvider.MemoryCache)
                {
                    services.AddScoped<ICacheAdapter, InMemoryCacheAdapter>();
                }
                else
                {
                    services.AddScoped<ICacheAdapter, DistributedCacheAdapter>();
                }

                switch (_hamoonConfigurations.Chaching.Provider)
                {
                    case ChachProvider.DistributedSqlServerCache:
                        services.AddDistributedSqlServerCache(options =>
                        {
                            options.ConnectionString = _hamoonConfigurations.Chaching.DistributedSqlServerCache.ConnectionString;
                            options.SchemaName = _hamoonConfigurations.Chaching.DistributedSqlServerCache.SchemaName;
                            options.TableName = _hamoonConfigurations.Chaching.DistributedSqlServerCache.TableName;
                        });
                        break;
                    case ChachProvider.StackExchangeRedisCache:
                        services.AddStackExchangeRedisCache(options =>
                        {
                            options.Configuration = _hamoonConfigurations.Chaching.StackExchangeRedisCache.Configuration;
                            options.InstanceName = _hamoonConfigurations.Chaching.StackExchangeRedisCache.SampleInstance;
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
            var _hamoonConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.Where(type => type.Name == _hamoonConfigurations.JsonSerializerTypeName && typeof(IJsonSerializer).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }


        private static IServiceCollection AddObjectMapper(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var _hamoonConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if (_hamoonConfigurations.RegisterAutomapperProfiles)
            {
                services.AddAutoMapperProfiles(assembliesForSearch);
            }
            return services;
        }
        private static IServiceCollection AddUserInfoService(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _hamoonConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _hamoonConfigurations.UserInfoServiceTypeName && typeof(IUserInfoService).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            return services;
        }
        private static IServiceCollection AddTranslator(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _hamoonConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _hamoonConfigurations.Translator.TranslatorTypeName && typeof(ITranslator).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }


        private static IServiceCollection AddMessageBus(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _hamoonConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if (_hamoonConfigurations.MessageBus.Enabled)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _hamoonConfigurations.MessageBus.MessageConsumerTypeName && typeof(IMessageConsumer).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                 .AddClasses(classes => classes.Where(type => type.Name == _hamoonConfigurations.Messageconsumer.MessageInboxStoreTypeName && typeof(IMessageInboxItemRepository).IsAssignableFrom(type)))
                 .AsImplementedInterfaces()
                 .WithTransientLifetime());

                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(classes => classes.Where(type => type.Name == _hamoonConfigurations.MessageBus.MessageBusTypeName && typeof(IMessageBus).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
            }
            return services;
        }

        private static IServiceCollection AddPoolingPublisher(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _hamoonConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if (_hamoonConfigurations.PoolingPublisher.Enabled)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(classes => classes.Where(type => type.Name == _hamoonConfigurations.PoolingPublisher.OutBoxRepositoryTypeName && typeof(IOutBoxEventItemRepository).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
                services.AddHostedService<PoolingPublisherHostedService>();

            }
            return services;
        }

        private static IServiceCollection AddEntityChangeInterception(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _hamoonConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurations>();
            if (_hamoonConfigurations.EntityChangeInterception.Enabled)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(classes => classes.Where(type => type.Name == _hamoonConfigurations.EntityChangeInterception.
                        EntityChageInterceptorRepositoryTypeName && typeof(IEntityChageInterceptorItemRepository).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
            }
            return services;
        }
    }
}
