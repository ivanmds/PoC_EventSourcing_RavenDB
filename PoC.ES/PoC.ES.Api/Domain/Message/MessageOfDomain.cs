namespace PoC.ES.Api.Domain.Message
{
    public class MessageOfDomain
    {
        public static (string Code, string Message) Success => ("200", "Success");
        public static (string Code, string Message) AlreadyItem => ("400", "Already item in collection");
        public static (string Code, string Message) InvalidItem => ("401", "Item is invalid");
    }
}
