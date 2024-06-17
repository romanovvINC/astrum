﻿using Astrum.SharedLib.Domain.Interfaces;

namespace Astrum.SharedLib.Application.Contracts.Infrastructure.EventStore;

public interface IRetroactiveEventsService
{
    /// <summary>
    ///     Apply retroactive events to the event stream
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAggregateId"></typeparam>
    /// <param name="eventStream"></param>
    /// <returns>An event stream with the retroactive events injected</returns>
    IReadOnlyCollection<IDomainEvent<TAggregateId>> ApplyRetroactiveEventsToStream<T, TAggregateId>(
        IReadOnlyCollection<IDomainEvent<TAggregateId>> eventStream) where T : class, IAggregateRoot<TAggregateId>;
}