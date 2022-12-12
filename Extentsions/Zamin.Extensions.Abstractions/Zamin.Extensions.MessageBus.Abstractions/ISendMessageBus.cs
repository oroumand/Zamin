namespace Zamin.Extensions.MessageBus.Abstractions;
/// <summary>
/// جهت ارسال پیام از این زیر ساخت اتستفاده می‌شود
/// پیام ها می‌توانند در قالب دستوری باشند که به سرویسی خاص ارسال می‌شوند یا پیامی عمومی که برای همه ارسال می‌شود
/// </summary>
public interface ISendMessageBus
{
    void Publish<TInput>(TInput input);
    void SendCommandTo<TCommandData>(string destinationService, string commandName, TCommandData commandData);
    void SendCommandTo<TCommandData>(string destinationService, string commandName, string correlationId, TCommandData commandData);
    void Send(Parcel parcel);
}
