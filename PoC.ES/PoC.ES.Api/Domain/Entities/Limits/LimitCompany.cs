namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class LimitCompany: LimitConfiguration
    {
        public LimitCompany(string companyKey) : base(companyKey) => Id = companyKey;

        public static LimitCompany Create(string companyKey) => new LimitCompany(companyKey);
    }
}
