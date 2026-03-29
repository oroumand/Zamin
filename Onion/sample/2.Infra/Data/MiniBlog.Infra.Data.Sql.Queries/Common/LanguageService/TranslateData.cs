using Newtonsoft.Json.Linq;

namespace MiniBlog.Infra.Data.Sql.Queries.Common.LanguageService
{
    public static class TranslateData
    {
        public static string Getvalue(string jsonValue, string cultureCode)
        {
            try
            {
                if (string.IsNullOrEmpty(cultureCode))
                    return jsonValue;
                JObject keyValuePairs = JObject.Parse(jsonValue);
                JToken jToken = keyValuePairs[cultureCode.Substring(0, 2)];
                return jToken.ToString();
            }
            catch (Exception ex)
            {

                return jsonValue;
            }

        }

    }
}