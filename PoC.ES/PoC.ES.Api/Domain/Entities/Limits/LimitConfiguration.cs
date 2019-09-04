using PoC.ES.Api.Domain.Message;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<(string Code, string Message)> AddLimits(IEnumerable<Limit> limits)
        {
            foreach (var limit in limits)
                yield return AddLimit(limit);
        }

        public (string Code, string Message) AddLimit(Limit limit)
        {
            var alreadyHave = _limits.Any(l => l.Equals(limit));
            if (alreadyHave) return MessageOfDomain.AlreadyItem;

            _limits.Add(limit);
            return MessageOfDomain.Success;
        }
    }
}
