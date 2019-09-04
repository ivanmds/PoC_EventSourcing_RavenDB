using PoC.ES.Api.Domain.Types;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Entities
{
    public abstract class LimitConfiguration
    {
        protected LimitConfiguration(string companyKey)
            : this(companyKey, "") { }

        protected LimitConfiguration(string companyKey, string documentNumber)
        {
            CompanyKey = companyKey;
            DocumentNumber = documentNumber;
            Type = GetTypeConfiguration();
            Id = GetId();
        }

        public string Id { get; private set; }
        public string CompanyKey { get; private set; }
        public string DocumentNumber { get; private set; }
        public LimitConfigurationType Type { get; private set; }


        private List<Limit> _lists = new List<Limit>();
        public IReadOnlyCollection<Limit> Limits
        {
            get => _lists;
            private set
            {
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

        protected LimitConfigurationType GetTypeConfiguration()
        {
            return !string.IsNullOrEmpty(CompanyKey) && !string.IsNullOrEmpty(DocumentNumber)
                ? LimitConfigurationType.Customer : LimitConfigurationType.Company;
        }

        protected string GetId()
        {
            return !string.IsNullOrEmpty(CompanyKey) && !string.IsNullOrEmpty(DocumentNumber)
                ? $"{CompanyKey}#{DocumentNumber}" : CompanyKey;
        }
    }
}
