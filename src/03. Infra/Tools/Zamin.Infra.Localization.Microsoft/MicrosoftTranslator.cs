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
        public MicrosoftTranslator(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            using var serviceScop = serviceScopeFactory.CreateScope();
            var stringLocalizerFactory = serviceScop.ServiceProvider.GetRequiredService<IStringLocalizerFactory>();
            var assemblyQualifiedName = configuration["ZaminConfigurations:MicrosoftTranslator:ResourceKeyHolderAssemblyQualifiedTypeName"];
            var type = Type.GetType(assemblyQualifiedName);
            _localizer = stringLocalizerFactory.Create(type);
        }
        public string this[string name] { get => GetString(name); set => throw new NotImplementedException(); }
        public string this[string name, params string[] arguments] { get => GetString(name, arguments); set => throw new NotImplementedException(); }
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
    }
}