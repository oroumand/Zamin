namespace Zamin.Utilities.Services.Localizations
{
    public interface ITranslator
    {
        string this[string name]
        {
            get;
            set;
        }
        string this[string name, params string[] arguments]
        {
            get;
            set;
        }

        string this[char separator, params string[] names]
        {
            get;
            set;
        }

        string GetString(string name);
        string GetString(string pattern, params string[] arguments);
        string GetConcateString(char separator = ' ', params string[] names);
    }
}
