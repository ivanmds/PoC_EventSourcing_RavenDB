namespace PoC.ES.Api.Domain.Entities
{
    public class Customer : LimitConfiguration
    {
        protected Customer(string companyKey, string documentNumber, bool registrationCompleted = false)
            :base(companyKey, documentNumber)
        {
            RegistrationCompleted = registrationCompleted;
        }

        public bool RegistrationCompleted { get; private set; }

        public static Customer Create(string companyKey, string documentNumber, bool registrationCompleted = false)
            => new Customer(companyKey, documentNumber, registrationCompleted);
    }
}
