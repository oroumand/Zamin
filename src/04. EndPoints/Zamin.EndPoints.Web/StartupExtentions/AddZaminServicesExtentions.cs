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
using Microsoft.AspNetCore.Http;

namespace Zamin.EndPoints.Web.StartupExtentions
{
    public static class AddZaminServicesExtentions
    {
        public static IServiceCollection AddZaminServices(
           this IServiceCollection services,
           IEnumerable<Assembly> assembliesForSearch)
        {
            services.AddCaching();
            services.AddSession();
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
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();
            if (_zaminConfigurations?.Caching?.Enable == true)
            {
                if (_zaminConfigurations.Caching.Provider == CacheProvider.MemoryCache)
                {
                    services.AddScoped<ICacheAdapter, InMemoryCacheAdapter>();
                }
                else
                {
                    services.AddScoped<ICacheAdapter, DistributedCacheAdapter>();
                }

                switch (_zaminConfigurations.Caching.Provider)
                {
                    case CacheProvider.DistributedSqlServerCache:
                        services.AddDistributedSqlServerCache(options =>
                        {
                            options.ConnectionString = _zaminConfigurations.Caching.DistributedSqlServerCache.ConnectionString;
                            options.SchemaName = _zaminConfigurations.Caching.DistributedSqlServerCache.SchemaName;
                            options.TableName = _zaminConfigurations.Caching.DistributedSqlServerCache.TableName;
                        });
                        break;
                    case CacheProvider.StackExchangeRedisCache:
                        services.AddStackExchangeRedisCache(options =>
                        {
                            options.Configuration = _zaminConfigurations.Caching.StackExchangeRedisCache.Configuration;
                            options.InstanceName = _zaminConfigurations.Caching.StackExchangeRedisCache.SampleInstance;
                        });
                        break;
                    case CacheProvider.NCacheDistributedCache:
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
        private static IServiceCollection AddSession(this IServiceCollection services)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();
            if (_zaminConfigurations?.Session?.Enable == true)
            {
                var sessionCookie = _zaminConfigurations.Session.Cookie;
                CookieBuilder cookieBuilder = new();
                cookieBuilder.Name = sessionCookie.Name;
                cookieBuilder.Domain = sessionCookie.Domain;
                cookieBuilder.Expiration = sessionCookie.Expiration;
                cookieBuilder.HttpOnly = sessionCookie.HttpOnly;
                cookieBuilder.IsEssential = sessionCookie.IsEssential;
                cookieBuilder.MaxAge = sessionCookie.MaxAge;
                cookieBuilder.Path = sessionCookie.Path;
                cookieBuilder.SameSite = Enum.Parse<SameSiteMode>(sessionCookie.SameSite.ToString());
                cookieBuilder.SecurePolicy = Enum.Parse<CookieSecurePolicy>(sessionCookie.SecurePolicy.ToString());

                services.AddSession(options =>
                {
                    options.Cookie = cookieBuilder;
                    options.IdleTimeout = _zaminConfigurations.Session.IdleTimeout;
                    options.IOTimeout = _zaminConfigurations.Session.IOTimeout;
                });
            }
            return services;
        }
        private static IServiceCollection AddLogging(this IServiceCollection services)
        {
            return services.AddScoped<IScopeInformation, ScopeInformation>();
        }

        public static IServiceCollection AddJsonSerializer(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.Where(type => type.Name == _zaminConfigurations.JsonSerializerTypeName && typeof(IJsonSerializer).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }


        private static IServiceCollection AddObjectMapper(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();
            if (_zaminConfigurations.RegisterAutomapperProfiles)
            {
                services.AddAutoMapperProfiles(assembliesForSearch);
            }
            return services;
        }
        private static IServiceCollection AddUserInfoService(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _zaminConfigurations.UserInfoServiceTypeName && typeof(IUserInfoService).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            return services;
        }
        private static IServiceCollection AddTranslator(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _zaminConfigurations.Translator.TranslatorTypeName && typeof(ITranslator).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }


        private static IServiceCollection AddMessageBus(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();

            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _zaminConfigurations.MessageBus.MessageConsumerTypeName && typeof(IMessageConsumer).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.Scan(s => s.FromAssemblies(assembliesForSearch)
             .AddClasses(classes => classes.Where(type => type.Name == _zaminConfigurations.Messageconsumer.MessageInboxStoreTypeName && typeof(IMessageInboxItemRepository).IsAssignableFrom(type)))
             .AsImplementedInterfaces()
             .WithTransientLifetime());

            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(classes => classes.Where(type => type.Name == _zaminConfigurations.MessageBus.MessageBusTypeName && typeof(IMessageBus).IsAssignableFrom(type)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }

        private static IServiceCollection AddPoolingPublisher(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();
            if (_zaminConfigurations.PoolingPublisher.Enabled)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(classes => classes.Where(type => type.Name == _zaminConfigurations.PoolingPublisher.OutBoxRepositoryTypeName && typeof(IOutBoxEventItemRepository).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
                services.AddHostedService<PoolingPublisherHostedService>();

            }
            return services;
        }

        private static IServiceCollection AddEntityChangeInterception(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch)
        {
            var _zaminConfigurations = services.BuildServiceProvider().GetService<ZaminConfigurationOptions>();
            if (_zaminConfigurations.EntityChangeInterception.Enabled)
            {
                services.Scan(s => s.FromAssemblies(assembliesForSearch)
                    .AddClasses(classes => classes.Where(type => type.Name == _zaminConfigurations.EntityChangeInterception.
                        EntityChageInterceptorRepositoryTypeName && typeof(IEntityChageInterceptorItemRepository).IsAssignableFrom(type)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
            }
            return services;
        }
    }
}
