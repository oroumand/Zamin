using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Zamin.Toolkits.Services.Serializers;

namespace Zamin.Infra.Tools.Srlzr.NewtonSoft
{
    public class NewtonSoftSerializer : IJsonSerializer
    {
        public TOutput Deserialize<TOutput>(string input) =>
            string.IsNullOrWhiteSpace(input) ?
                default : JsonConvert.DeserializeObject<TOutput>(input);
        public string Serilize<TInput>(TInput input) => input == null ? string.Empty : JsonConvert.SerializeObject(input, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
    }
}
