using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Message;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Limit : IEquatable<Limit>
    {
        public Limit(LimitType type, FeatureType featureType, bool registrationCompleted = false)
        {
            Type = type;
            FeatureType = featureType;
            RegistrationCompleted = registrationCompleted;
        }

        public LimitType Type { get; private set; }
        public FeatureType FeatureType { get; private set; }
        public bool RegistrationCompleted { get; private set; }

        private List<Cycle> _cycles = new List<Cycle>();
        public IReadOnlyCollection<Cycle> Cycles
        {
            get => _cycles;
            private set => _cycles.AddRange(value);
        }

        public IEnumerable<(string Code, string Message)> AddCycles(IEnumerable<Cycle> cycles)
        {
            foreach (var cycle in cycles)
                yield return AddCycle(cycle);
        }

        public (string Code, string Message) AddCycle(Cycle cycle)
        {
            var alreadyHave = _cycles.Any(c => c.Equals(cycle));
            if (alreadyHave) return MessageOfDomain.AlreadyItem;

            _cycles.Add(cycle);
            return MessageOfDomain.Success;
        }

        public bool Equals(Limit other)
        {
            return Type == other.Type && FeatureType == other.FeatureType;
        }

        public static Limit Create(LimitType type, FeatureType featureType, bool registrationCompleted = false) =>
           new Limit(type, featureType, registrationCompleted);
    }
}
