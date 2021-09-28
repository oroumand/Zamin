using Zamin.Utilities.Configurations;
using Zamin.Utilities.Services.Localizations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;

namespace Zamin.Infra.Localizations.Microsoft
{
    public class MicrosoftTranslator : ITranslator
    {
        private readonly IStringLocalizer _localizer;
        public MicrosoftTranslator(IServiceProvider serviceProvider, ZaminConfigurationOptions zaminConfigurationOptions)
        {
            var stringLocalizerFactory = serviceProvider.GetRequiredService<IStringLocalizerFactory>();
            var type = Type.GetType(zaminConfigurationOptions.Translator.MicrosoftTranslatorOptions.ResourceKeyHolderAssemblyQualifiedTypeName);
            _localizer = stringLocalizerFactory.Create(type);
        }
        public string this[string name] { get => GetString(name); set => throw new NotImplementedException(); }
        public string this[string name, params string[] arguments] { get => GetString(name, arguments); set => throw new NotImplementedException(); }
        public string this[char sepprator, params string[] names] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetString(string name)
        {
            return _localizer[name];
        }
        public string GetString(string name, params string[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                arguments[i] = GetString(arguments[i]);
            }
            return _localizer[name, arguments];
        }

        public string GetConcateString(char sepprator, params string[] names)
        {
            throw new NotImplementedException();
        }
    }
}