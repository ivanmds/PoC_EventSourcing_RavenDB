namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Company: LimitConfiguration
    {
        public Company(string companyKey) : base(companyKey) => Id = companyKey;

        public static Company Create(string companyKey) => new Company(companyKey);
    }
}
