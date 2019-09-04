using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public abstract class LimitConfiguration : AggregateRoot
    {
        protected LimitConfiguration(string companyKey) => CompanyKey = companyKey;
        public string CompanyKey { get; private set; }

        private List<Limit> _limits = new List<Limit>();
        public IReadOnlyCollection<Limit> Limits
        {
            get => _limits;
            private set => _limits.AddRange(value);
        }

        public void AddLimits(IEnumerable<Limit> limits) => _limits.AddRange(limits);

        public void AddLimit(Limit limit) => _limits.Add(limit);
    }
}
