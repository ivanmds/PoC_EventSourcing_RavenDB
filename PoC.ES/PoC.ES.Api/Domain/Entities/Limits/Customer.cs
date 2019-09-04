using PoC.ES.Api.Domain.Types.Limits;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Customer : LimitConfiguration
    {
        public Customer(string companyKey, string documentNumber, bool registrationCompleted = false)
            :base(companyKey)
        {
            RegistrationCompleted = registrationCompleted;
            DocumentNumber = documentNumber;
            Id = GetId();
        }

        public bool RegistrationCompleted { get; private set; }
        public string DocumentNumber { get; private set; }

        public static Customer Create(string companyKey, string documentNumber, bool registrationCompleted = false)
            => new Customer(companyKey, documentNumber, registrationCompleted);

        protected string GetId() => $"{CompanyKey}#{DocumentNumber}";
    }
}
