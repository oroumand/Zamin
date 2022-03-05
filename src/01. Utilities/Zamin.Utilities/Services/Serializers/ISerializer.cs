namespace Zamin.Utilities.Services.Serializers;
public interface IJsonSerializer
{
    string Serialize<TInput>(TInput input);

    string Serialize<TInput, TJsonOption>(TInput input , TJsonOption jsonOption);

    TOutput Deserialize<TOutput>(string input);

    TOutput Deserialize<TOutput, TJsonOption>(string input, TJsonOption jsonOption);

    object Deserialize(string input, Type type);
}