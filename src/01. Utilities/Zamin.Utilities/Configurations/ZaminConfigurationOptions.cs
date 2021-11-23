using System.Text.Json;

namespace Zamin.Utilities.Configurations;
public class ZaminConfigurationOptions
{
    public string SectionName { get; set; } = "ZaminConfigurations";
    public string ServiceId { get; set; } = "Service01";
    public string ServiceName { get; set; } = "Service01";
    public string JsonSerializerTypeName { get; set; } = "NewtonSoftSerializer";
    public string ExcelSerializerTypeName { get; set; } = "EPPlusExcelSerializer";
    public string UserInfoServiceTypeName { get; set; } = "FakeUserInfoService";
    public bool RegisterAutomapperProfiles { get; set; } = true;
    public string AssmblyNameForLoad { get; set; } = string.Empty;
    public MessageBusOptions MessageBus { get; set; } = new MessageBusOptions();
    public MessageConsumerOptions Messageconsumer { get; set; } = new MessageConsumerOptions();
    public PoolingPublisherOptions PoolingPublisher { get; set; } = new PoolingPublisherOptions();
    public EntityChangeInterceptionOptions EntityChangeInterception { get; set; } = new EntityChangeInterceptionOptions();
    public ApplicationEventOptions ApplicationEvents { get; set; } = new ApplicationEventOptions();

    public TranslatorOptions Translator { get; set; } = new TranslatorOptions();
    public SwaggerOptions Swagger { get; set; } = new SwaggerOptions();
    public CachingOptions Caching { get; set; } = new CachingOptions();
    public SessionOptions Session { get; set; } = new SessionOptions();

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);

    }
}

