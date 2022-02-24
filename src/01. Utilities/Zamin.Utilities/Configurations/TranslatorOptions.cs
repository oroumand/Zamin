﻿namespace Zamin.Utilities.Configurations;
public class TranslatorOptions
{
    public string CultureInfo { get; set; } = "en-Us";
    public string TranslatorTypeName { get; set; }
    public ParrotTranslatorOptions Parrottranslator { get; set; } = new ParrotTranslatorOptions();
    public MicrosoftTranslatorOptions MicrosoftTranslatorOptions { get; set; } = null;
}


public class ParrotTranslatorOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool AutoCreateSqlTable { get; set; } = true;
    public string TableName { get; set; } = "ParrotTranslations";
    public string SchemaName { get; set; } = "dbo";

    public bool SeedTranslations = true;
    public Translation[] Translations { get; set; } = Array.Empty<Translation>();
}

public class Translation
{
    public string Key { get; set; }
    public string Value { get; set; }
    public string Culture { get; set; }
}

public class MicrosoftTranslatorOptions
{
    public string ResourceKeyHolderAssemblyQualifiedTypeName { get; set; }
}
