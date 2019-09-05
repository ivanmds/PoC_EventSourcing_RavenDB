namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class LimitCustomer : LimitConfiguration
    {
        public LimitCustomer(string companyKey, string documentNumber, bool registrationCompleted = false)
            :base(companyKey)
        {
            DocumentNumber = documentNumber;
            RegistrationCompleted = registrationCompleted;
            Id = GetId(CompanyKey, DocumentNumber);
        }

        public string DocumentNumber { get; private set; }
        public bool RegistrationCompleted { get; private set; }

        public static LimitCustomer Create(string companyKey, string documentNumber, bool registrationCompleted = false)
            => new LimitCustomer(companyKey, documentNumber, registrationCompleted);

        public static string GetId(string companyKey, string documentNumber) => $"{companyKey}#{documentNumber}";
    }
}
