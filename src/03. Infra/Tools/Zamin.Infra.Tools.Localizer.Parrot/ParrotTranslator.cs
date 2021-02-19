using Zamin.Utilities.Configurations;
using Zamin.Utilities.Services.Localizations;
using System;
using System.Globalization;

namespace Zamin.Infra.Tools.Localizer.Parrot
{
    public class ParrotTranslator : ITranslator
    {
        private readonly ParrotDataWrapper _localizer;
        private readonly string _currentCulture;
        public ParrotTranslator(ZaminConfigurations configuration)
        {
#if NET451
                        _currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
#elif NET46
                        _currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
#else
            _currentCulture = CultureInfo.CurrentCulture.ToString();
#endif
            _localizer = new ParrotDataWrapper(configuration);

        }
        public string this[string name] { get => GetString(name); set => throw new NotImplementedException(); }
        public string this[string name, params string[] arguments] { get => GetString(name, arguments); set => throw new NotImplementedException(); }
        public string this[char separator, params string[] names] { get => GetConcateString(separator, names); set => throw new NotImplementedException(); }
        public string GetString(string name)
        {
            return _localizer.Get(name, _currentCulture);
        }
        public string GetString(string pattern, params string[] arguments)
        {
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
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = GetString(names[i]);
            }
            return string.Join(separator, names);
        }
    }
}
