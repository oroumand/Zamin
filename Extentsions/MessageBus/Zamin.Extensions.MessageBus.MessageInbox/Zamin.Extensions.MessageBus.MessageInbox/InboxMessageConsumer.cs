﻿using Microsoft.Extensions.Options;
using System.Reflection;
using Zamin.Core.Contracts.ApplicationServices.Commands;
using Zamin.Core.Contracts.ApplicationServices.Events;
using Zamin.Core.Domain.Events;
using Zamin.Extensions.MessageBus.MessageInbox.Abstractions;
using Zamin.Extensions.MessageBus.MessageInbox.Abstractions.Options;
using Zamin.Extentions.MessageBus.Abstractions;
using Zamin.Extentions.Serializers.Abstractions;

namespace Zamin.Extensions.MessageBus.MessageInbox;
public class InboxMessageConsumer : IMessageConsumer
{
    private readonly MessageInboxOptions _messageInboxOptions;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IMessageInboxItemRepository _messageInboxItemRepository;
    private readonly List<Type> _domainEventTypes = new();
    private readonly List<Type> _commandTypes = new();

    public InboxMessageConsumer(IOptions<MessageInboxOptions> messageInboxOptions,
                                IJsonSerializer jsonSerializer,
                                IMessageInboxItemRepository messageInboxItemRepository,
                                ICommandDispatcher commandDispatcher = null,
                                IEventDispatcher eventDispatcher = null)
    {
        _messageInboxOptions = messageInboxOptions.Value;
        _eventDispatcher = eventDispatcher;
        _jsonSerializer = jsonSerializer;
        _commandDispatcher = commandDispatcher;
        _messageInboxItemRepository = messageInboxItemRepository;
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        _domainEventTypes.AddRange(assemblies.SelectMany(assembly => assembly.GetTypes().Where(c => c.IsAssignableTo(typeof(IDomainEvent)) && c.IsClass).ToList()).ToList());
        _commandTypes.AddRange(assemblies.SelectMany(assembly => assembly.GetTypes().Where(c => c.IsAssignableTo(typeof(ICommand)) && c.IsClass).ToList()).ToList());
    }

    public Task<bool> ConsumeCommandAsync(string sender, Parcel parcel)
    {
        throw new NotImplementedException();
        //if (_messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
        //{
        //    var mapToClass = _messageTypeMap[parcel.Route];
        //    var commandType = Type.GetType(mapToClass);
        //    dynamic command = _jsonSerializer.Deserialize(parcel.MessageBody, commandType);
        //    _commandDispatcher.Send(command);
        //    _messageInboxItemRepository.Receive(parcel.MessageId, sender);
        //}
    }

    public async Task<bool> ConsumeEventAsync(string sender, Parcel parcel)
    {
        var eventReceived = false;

        if (_messageInboxItemRepository.AllowReceive(parcel.MessageId, sender))
        {
            var eventType = _domainEventTypes.FirstOrDefault(c => c.Name == parcel.MessageName);
            if (eventType != null)
            {
                dynamic @event = _jsonSerializer.Deserialize(parcel.MessageBody, eventType);
                _eventDispatcher.PublishDomainEventAsync(@event);
                eventReceived = _messageInboxItemRepository.Receive(parcel.MessageId, sender);
            }
        }
        else
        {
            eventReceived = true;
        }

        return eventReceived;
    }
}