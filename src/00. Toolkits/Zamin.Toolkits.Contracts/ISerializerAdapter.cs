namespace Zamin.Toolkits.Contracts
{
    public interface ISerializerAdapter
    {
        string Serilize<TInput>(TInput input);
        string SerilizeObject(object input);
        TOutput Deserialize<TOutput>(string input);
        object DeserializeObject(string input);
    }
}
