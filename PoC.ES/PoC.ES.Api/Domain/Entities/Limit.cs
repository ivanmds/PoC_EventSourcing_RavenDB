namespace PoC.ES.Api.Domain.Entities
{
    public class Limit
    {
        public Limit(string type, int amount)
        {
            Type = type;
            Amount = amount;
        }

        public string Type { get; private set; }
        public int Amount { get; private set; }
    }
}
