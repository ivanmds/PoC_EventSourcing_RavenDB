namespace PoC.ES.Api.Domain.Entities
{
    public abstract class AggregateRoot
    {
        public string Id { get; protected set; }
    }
}
