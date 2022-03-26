using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;
using Zamin.Extensions.Translations.Parrot.Options;
using Zamin.Extentions.Translations.Abstractions;

namespace Zamin.Extensions.Translations.Parrot.Services;
public class ParrotTranslator : ITranslator,IDisposable
{
    private readonly ParrotSqlRepository _localizer;
    private readonly string _currentCulture;
    private readonly ILogger<ParrotTranslator> _logger;

    public string this[char separator, string culture, params string[] names] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string this[string name, string culture, params string[] arguments] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string this[string name, string culture] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public ParrotTranslator(IOptions<ParrotTranslatorOptions> configuration,ILogger<ParrotTranslator> logger)
    {
        _currentCulture = CultureInfo.CurrentCulture.ToString();
        _localizer = new ParrotSqlRepository(configuration.Value,logger);
        _logger = logger;
        _logger.LogInformation("Parrot Translator Start working with culture {Culture}", _currentCulture);
    }
    public string this[string name] { get => GetString(name); set => throw new NotImplementedException(); }
    public string this[string name, params string[] arguments] { get => GetString(name, arguments); set => throw new NotImplementedException(); }
    public string this[char separator, params string[] names] { get => GetConcateString(separator, names); set => throw new NotImplementedException(); }
    public string GetString(string name)
    {
        _logger.LogTrace("Parrot Translator GetString with name {name}", name);
        return _localizer.Get(name, _currentCulture);
    }
    public string GetString(string pattern, params string[] arguments)
    {
        _logger.LogTrace("Parrot Translator GetString with pattern {pattern} and arguments {arguments}", pattern,arguments);

        for (int i = 0; i < arguments.Length; i++)
        {
            arguments[i] = GetString(arguments[i]);
        }
        pattern = GetString(pattern);
        for (int i = 0; i < arguments.Length; i++)
        {
            string placeHolder = $"{{{i}}}";
            pattern = pattern.Replace(placeHolder, arguments[i]);
        }
        return pattern;
    }
    public string GetConcateString(char separator = ' ', params string[] names)
    {
        _logger.LogTrace("Parrot Translator GetConcateString with separator {separator} and names {names}", separator, names);

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = GetString(names[i]);
        }
        return string.Join(separator, names);
    }

    public string GetString(string name, string culture)
    {
        throw new NotImplementedException();
    }

    public string GetString(string pattern, string culture, params string[] arguments)
    {
        throw new NotImplementedException();
    }

    public string GetConcateString(string culture, char separator = ' ', params string[] names)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _logger.LogInformation("Parrot Translator Stop working with culture {Culture}", _currentCulture);
    }
}