using Zamin.Core.ApplicationServices.Events;
using Zamin.Utilities.Configurations;
using Zamin.Utilities.Services.MessageBus;
using Zamin.Utilities.Services.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using Zamin.Core.Contracts.ApplicationServices.Commands;

namespace Zamin.Messaging.IdempotentConsumers
{
    public class IdempotentMessageConsumer : IMessageConsumer
    {
        private readonly ZaminConfigurationOptions _zaminConfigurations;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IMessageInboxItemRepository _messageInboxItemRepository;
        private readonly Dictionary<string, string> _messageTypeMap = new Dictionary<string, string>();
        public IdempotentMessageConsumer(ZaminConfigurationOptions zaminConfigurations, IEventDispatcher eventDispatcher, IJsonSerializer jsonSerializer, ICommandDispatcher commandDispatcher, IMessageInboxItemRepository messageInboxItemRepository)
        {
            _zaminConfigurations = zaminConfigurations;
            _eventDispatcher = eventDispatcher;
            _jsonSerializer = jsonSerializer;
            _commandDispatcher = commandDispatcher;
            _messageInboxItemRepository = messageInboxItemRepository;
            LoadMessageMap();
        }

        private void LoadMessageMap()
        {

            if (_zaminConfigurations?.Messageconsumer?.Events?.Any() == true)
            {
                foreach (var eventPublisher in _zaminConfigurations?.Messageconsumer?.Events)
                {
                    foreach (var @event in eventPublisher?.EventData)
                    {
                        _messageTypeMap.Add($"{eventPublisher.FromServiceId}.{@event.EventName}", @event.MapToClass);

                    }
                }
            }
            if (_zaminConfigurations?.Messageconsumer?.Commands?.Any() == true)
            {
                foreach (var item in _zaminConfigurations?.Messageconsumer?.Commands)
                {
                    _messageTypeMap.Add($"{_zaminConfigurations.ServiceId}.{item.CommandName}", item.MapToClass);
                }
            }
        }

        public void ConsumeCommand(string sender, Parcel parcel)
        {
            if (_messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
            {
                var mapToClass = _messageTypeMap[parcel.Route];
                var commandType = Type.GetType(mapToClass);
                dynamic command = _jsonSerializer.Deserialize(parcel.MessageBody, commandType);
                _commandDispatcher.Send(command);
                _messageInboxItemRepository.Receive(parcel.MessageId, sender);
            }
        }

        public void ConsumeEvent(string sender, Parcel parcel)
        {
            if (_messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
            {
                var mapToClass = _messageTypeMap[parcel.Route];
                var eventType = Type.GetType(mapToClass);
                dynamic @event = _jsonSerializer.Deserialize(parcel.MessageBody, eventType);
                _eventDispatcher.PublishDomainEventAsync(@event);
                _messageInboxItemRepository.Receive(parcel.MessageId, sender);
            }
        }
    }
}
