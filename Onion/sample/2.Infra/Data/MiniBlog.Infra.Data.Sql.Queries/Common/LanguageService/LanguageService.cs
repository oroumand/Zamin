using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamin.Extensions.Translations.Abstractions;

namespace MiniBlog.Infra.Data.Sql.Queries.Common.LanguageService
{
    public class LanguageService : ILanguageService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITranslator _translator;

        public LanguageService(IHttpContextAccessor httpContextAccessor, ITranslator translator)
        {
            _httpContextAccessor = httpContextAccessor;
            _translator = translator;
        }
        public string GetLanguage()
        {
            StringValues lang = string.Empty;
            _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Accept-Language", out lang);

            return string.IsNullOrEmpty(lang) ? "fa-IR" : lang.ToString().Substring(0, 5);

        }
         

    }
}
