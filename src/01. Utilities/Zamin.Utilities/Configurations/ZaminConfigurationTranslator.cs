using System;
using System.Collections.Generic;
using System.Text;

namespace Zamin.Utilities.Configurations
{
    public class Translator
    {
        public string CultureInfo { get; set; } = "en-Us";
        public string TranslatorTypeName { get; set; }
        public Parrottranslator Parrottranslator { get; set; }
    }


    public class Parrottranslator
    {
        public string ConnectionString { get; set; }
        public bool AutoCreateSqlTable { get; set; } = true;
        public string TableName { get; set; } = "ParrotTranslations";
        public string SchemaName { get; set; } = "dbo";
    }

}
