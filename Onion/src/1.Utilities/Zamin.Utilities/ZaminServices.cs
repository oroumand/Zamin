using Microsoft.Extensions.Logging;
using Zamin.Extensions.Caching.Abstractions;
using Zamin.Extentions.ObjectMappers.Abstractions;
using Zamin.Extentions.Serializers.Abstractions;
using Zamin.Extentions.Translations.Abstractions;
using Zamin.Extentions.UsersManagement.Abstractions;

namespace Zamin.Utilities;

public class ZaminServices
{
    public readonly ITranslator Translator;
    public readonly ICacheAdapter CacheAdapter;
    public readonly IMapperAdapter MapperFacade;
    public readonly ILoggerFactory LoggerFactory;
    public readonly IJsonSerializer Serializer;
    public readonly IUserInfoService UserInfoService;

    public ZaminServices(ITranslator translator,
            ILoggerFactory loggerFactory,
            IJsonSerializer serializer,
            IUserInfoService userInfoService,
            ICacheAdapter cacheAdapter,
            IMapperAdapter mapperFacade)
    {
        Translator = translator;
        LoggerFactory = loggerFactory;
        Serializer = serializer;
        UserInfoService = userInfoService;
        CacheAdapter = cacheAdapter;
        MapperFacade = mapperFacade;
    }
}