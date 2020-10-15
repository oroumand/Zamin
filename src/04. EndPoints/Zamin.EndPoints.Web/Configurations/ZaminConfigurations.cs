using System;
using System.Linq;

namespace Zamin.EndPoints.Web.Configurations
{

    public class Translator
    {
        public string CultureInfo { get; set; } = "en-Us";
        public string TranslatorTypeName { get; set; }
        public Parrottranslator Parrottranslator { get; set; }
    }

    public class Parrottranslator
    {
        public string ConnectionString { get; set; }
    }



    public class ZaminConfigurations
    {
        public Translator Translator { get; set; }

        public string JsonSerializerTypeName { get; set; }
        public string ExcelSerializerTypeName { get; set; }
        public string UserInfoServiceTypeName { get; set; }

        public bool UseFakeUserService { get; set; } = true;
        public bool RegisterRepositories { get; set; } = true;
        public bool RegisterHandlers { get; set; } = true;
        public bool RegisterAutomapperProfiles { get; set; } = true;
        public Swagger Swagger { get; set; }
        public Chaching Chaching { get; set; }
        public string AssmblyNameForLoad { get; set; }
    }


    public class Swagger
    {
        public Swaggerdoc SwaggerDoc { get; set; }
    }

    public class Swaggerdoc
    {
        public string Version { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }




    public enum ChachProvider
    {
        MemoryCache,
        DistributedSqlServerCache,
        StackExchangeRedisCache,
        NCacheDistributedCache
    }
    public class Chaching
    {
        public bool Enable { get; set; }
        public bool EnableQueryAutomaticCache { get; set; }
        public ChachProvider Provider { get; set; }
        public Distributedsqlservercache DistributedSqlServerCache { get; set; }
        public Stackexchangerediscache StackExchangeRedisCache { get; set; }
        public Ncachedistributedcache NCacheDistributedCache { get; set; }
        public Policy[] Policies { get; set; }

        public Policy GetFor(string typeName) =>
             Policies?.Length < 1 ? null :
                 Policies.OrderBy(c => c.Order).FirstOrDefault(c =>
                 c?.Excludes?.Any(d => typeName.Contains(d)) == false &&
                 (c?.Includes?.Any(d => typeName.Contains(d)) == true ||
                 c?.Includes?.Contains("*") == true));

    }

    public class Distributedsqlservercache
    {
        public string ConnectionString { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
    }

    public class Stackexchangerediscache
    {
        public string Configuration { get; set; }
        public string SampleInstance { get; set; }
    }

    public class Ncachedistributedcache
    {
        public string CacheName { get; set; }
        public bool EnableLogs { get; set; }
        public bool ExceptionsEnabled { get; set; }
    }

    public class Policy
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public DateTime? AbsoluteExpiration { get; set; }
        public int SlidingExpiration { get; set; }
        public string[] Includes { get; set; }
        public string[] Excludes { get; set; }
    }


}
