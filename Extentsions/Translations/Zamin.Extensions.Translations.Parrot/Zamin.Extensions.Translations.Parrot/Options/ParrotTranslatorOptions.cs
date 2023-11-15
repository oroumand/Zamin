namespace Zamin.Extensions.Translations.Parrot.Options;
public class ParrotTranslatorOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool AutoCreateSqlTable { get; set; } = true;
    public string TableName { get; set; } = "ParrotTranslations";
    public string SchemaName { get; set; } = "dbo";
    public int ReloadDataIntervalInMinuts { get; set; }
    public DefaultTranslationOption[] DefaultTranslations { get; set; } = Array.Empty<DefaultTranslationOption>();

}

public class DefaultTranslationOption
{
    public string Key { get; set; } = nameof(Key);
    public string Value { get; set; } = nameof(Value);
    public string Culture { get; set; } = nameof(Culture);
}