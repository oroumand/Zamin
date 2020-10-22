namespace Zamin.Utilities.Services.Serializers
{
    public interface IJsonSerializer
    {
        string Serilize<TInput>(TInput input);
        TOutput Deserialize<TOutput>(string input);
    }
}
