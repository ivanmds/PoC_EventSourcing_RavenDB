using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Limit
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

        public void AddCycle(Cycle cycle) => _cycles.Add(cycle);

        public static Limit Create(LimitType type, FeatureType featureType, bool registrationCompleted = false) =>
            new Limit(type, featureType, registrationCompleted);
    }
}
