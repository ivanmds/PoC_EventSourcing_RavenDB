using PoC.ES.Api.Domain.Entities;
using System;

namespace PoC.ES.Api.Domain.Events
{
    public abstract class EventBase : AggregateRoot
    {
        public DateTime Timestamp { get => DateTime.Now; }
        public string AggregateRootId { get; protected set; }
        public string DataJson { get; protected set; }
        public string Name { get; protected set; }
    }
}
