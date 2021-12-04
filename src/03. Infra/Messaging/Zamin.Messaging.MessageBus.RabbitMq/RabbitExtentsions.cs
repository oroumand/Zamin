using Zamin.Utilities.Services.MessageBus;
using System.Text;
using RabbitMQ.Client.Events;

namespace Zamin.Messaging.MessageBus.RabbitMq;
static class RabbitExtentsions
{
    public static Parcel ToParcel(this BasicDeliverEventArgs basicDeliverEventArgs)
    {
        Parcel parcel = new Parcel
        {
            CorrelationId = basicDeliverEventArgs?.BasicProperties?.CorrelationId,
            MessageId = basicDeliverEventArgs?.BasicProperties.MessageId,
            Route = basicDeliverEventArgs.RoutingKey,
            MessageBody = Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray()),
            MessageName = basicDeliverEventArgs.BasicProperties.Type,
            Headers = basicDeliverEventArgs?.BasicProperties?.Headers != null ? (Dictionary<string, object>)basicDeliverEventArgs?.BasicProperties?.Headers : null
        };
        return parcel;
    }
}
