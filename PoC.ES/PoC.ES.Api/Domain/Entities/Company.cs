namespace PoC.ES.Api.Domain.Entities
{
    public class Company: LimitConfiguration
    {
        protected Company(string companyKey) : base(companyKey) { }

        public static Company Create(string companyKey) => new Company(companyKey);
    }
}
