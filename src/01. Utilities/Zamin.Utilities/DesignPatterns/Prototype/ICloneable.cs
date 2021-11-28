namespace Zamin.Utilities.DesignPatterns.Prototype;
/// <summary>
/// در صورت نیاز به استفاده از الگوی Prototype که نیاز به کپی عمیق از شی دارد می‌تواند از این Interface استفاده کرد
/// </summary>
/// <typeparam name="T">نوع هدفی که قرار است کپی عمیق از آن تهیه شود</typeparam>
public interface ICloneable<T>
{
    T Clone();
}
