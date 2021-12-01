using Zamin.Utilities.Services.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Zamin.Infra.Tools.Srlzr.NewtonSoft;
public class NewtonSoftSerializer : IJsonSerializer
{
Zamin.Infra.Tools.Srlzr.NewtonSoft
    public TOutput Deserialize<TOutput>(string input) =>
        string.IsNullOrWhiteSpace(input) ?
            default : JsonConvert.DeserializeObject<TOutput>(input);

    public object Deserialize(string input, Type type) =>
        string.IsNullOrWhiteSpace(input) ?
            null : JsonConvert.DeserializeObject(input, type);

    public string Serilize<TInput>(TInput input) => input == null ? string.Empty : JsonConvert.SerializeObject(input, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
}
