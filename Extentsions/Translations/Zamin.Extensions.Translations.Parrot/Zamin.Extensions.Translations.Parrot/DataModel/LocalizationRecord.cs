namespace Zamin.Extensions.Translations.Parrot.DataModel;
public class LocalizationRecord
{
    public long Id { get; set; }
    public Guid BusinessId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Culture { get; set; } = string.Empty;
    public bool CorrespondingValueNotFound => string.IsNullOrEmpty(Value);
    public override string ToString() => Value.ToString();
}
