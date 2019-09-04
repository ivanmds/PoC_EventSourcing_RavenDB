using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public abstract class LimitConfiguration : AggregateRoot
    {
        protected LimitConfiguration(string companyKey) => CompanyKey = companyKey;
        public string CompanyKey { get; private set; }

        private List<Limit> _lists = new List<Limit>();
        public IReadOnlyCollection<Limit> Limits
        {
            get => _lists;
            private set => _lists.AddRange(value);
        }

        public void AddLimit(Limit limit) => _lists.Add(limit);
    }
}
