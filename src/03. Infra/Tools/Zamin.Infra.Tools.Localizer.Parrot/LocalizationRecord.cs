namespace Zamin.Infra.Tools.Localizer.Parrot;
public class LocalizationRecord
{
    public int Id { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public string Culture { get; set; }
    public bool CorrespondingValueNotFound => string.IsNullOrEmpty(Value);
    public override string ToString() => Value?.ToString();
}
