namespace Zamin.Extentions.Translations.Abstractions;
public interface ITranslator
{
    string this[string name]
    {
        get;
        set;
    }
    string this[string name, string culture]
    {
        get;
        set;
    }
    string this[string name, params string[] arguments]
    {
        get;
        set;
    }
    string this[string name, string culture, params string[] arguments]
    {
        get;
        set;
    }
    string this[char separator, params string[] names]
    {
        get;
        set;
    }
    string this[char separator, string culture, params string[] names]
    {
        get;
        set;
    }
    string GetString(string name);
    string GetString(string name, string culture);
    string GetString(string pattern, params string[] arguments);
    string GetString(string pattern, string culture, params string[] arguments);
    string GetConcateString(char separator = ' ', params string[] names);
    string GetConcateString(string culture,char separator = ' ', params string[] names);
}
