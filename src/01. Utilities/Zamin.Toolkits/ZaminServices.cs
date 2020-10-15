using Microsoft.Extensions.Logging;
using Zamin.Toolkits.Services.Chaching;
using Zamin.Toolkits.Services.ObjectMappers;
using Zamin.Toolkits.Services.Serializers;
using Zamin.Toolkits.Services.Translations;
using Zamin.Toolkits.Services.Users;

namespace Zamin.Toolkits
{
    public class ZaminServices
    {
        public readonly ITranslator ResourceManager;
        public readonly ICacheAdapter CacheAdapter;
        public readonly IMapperAdapter MapperFacade;
        public readonly ILoggerFactory LoggerFactory;
        public readonly IJsonSerializer Serializer;
        public readonly IUserInfoService UserInfoService;

        public ZaminServices(ITranslator resourceManager,
                ILoggerFactory loggerFactory,
                IJsonSerializer serializer,
                IUserInfoService userInfoService,
                ICacheAdapter cacheAdapter,
                IMapperAdapter mapperFacade)
        {
            ResourceManager = resourceManager;
            LoggerFactory = loggerFactory;
            Serializer = serializer;
            UserInfoService = userInfoService;
            CacheAdapter = cacheAdapter;
            MapperFacade = mapperFacade;
        }
    }
}
