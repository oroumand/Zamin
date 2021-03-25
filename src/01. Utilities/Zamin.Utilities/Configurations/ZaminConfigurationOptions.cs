namespace Zamin.Utilities.Configurations
{


    public class ZaminConfigurationOptions
    {
        public string SectionName { get; set; } = "ZaminConfigurations";
        public string ServiceId { get; set; } = "Service01";
        public string ServiceName { get; set; } = "Service01";
        public string JsonSerializerTypeName { get; set; }
        public string ExcelSerializerTypeName { get; set; }
        public string UserInfoServiceTypeName { get; set; }
        public bool RegisterAutomapperProfiles { get; set; } = true;
        public string AssmblyNameForLoad { get; set; }
        public MessageBusOptions MessageBus { get; set; }
        public MessageConsumerOptions Messageconsumer { get; set; }
        public PoolingPublisherOptions PoolingPublisher { get; set; }
        public EntityChangeInterceptionOptions EntityChangeInterception { get; set; }
        public ApplicationEventOptions ApplicationEvents { get; set; }

        public TranslatorOptions Translator { get; set; }
        public SwaggerOptions Swagger { get; set; }
        public CachingOptions Caching { get; set; }
        public SessionOptions Session { get; set; }
    }
}
