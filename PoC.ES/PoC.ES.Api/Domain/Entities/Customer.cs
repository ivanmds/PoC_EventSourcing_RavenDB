using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Entities
{
    public class Customer
    {
        public Customer(string companyKey, string documentNumber, bool registrationCompleted = false)
        {
            CompanyKey = companyKey;
            DocumentNumber = documentNumber;
            RegistrationCompleted = registrationCompleted;
        }

        public string Id { get; private set; }
        public string CompanyKey { get; private set; }
        public string DocumentNumber { get; private set; }
        public bool RegistrationCompleted { get; private set; }

        private List<Limit> _lists = new List<Limit>();
        public IReadOnlyCollection<Limit> Limits
        {
            get => _lists;
            private set
            {
                _lists.Clear();
                _lists.AddRange(value);
            }
        }

       
        public void AddLimits(IEnumerable<Limit> limits)
        {
            _lists.AddRange(limits);
        }

        public void AddLimit(Limit limit)
        {
            _lists.Add(limit);
        }
    }
}
