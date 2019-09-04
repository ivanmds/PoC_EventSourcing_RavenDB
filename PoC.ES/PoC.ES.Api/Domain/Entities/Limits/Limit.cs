using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Message;
using PoC.ES.Api.Domain.Validation;
using PoC.ES.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Limit : IEquatable<Limit>, IValidated
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

        public void AddCycles(IEnumerable<Cycle> cycles) => _cycles.AddRange(cycles);
        public void AddCycle(Cycle cycle) => _cycles.Add(cycle);

        public bool Equals(Limit other)
        {
            return Type == other.Type && FeatureType == other.FeatureType;
        }

        public ResultOfCommand Validate()
        {
            var result = ResultOfCommand.Create();

            foreach (var cycle in Cycles)
            {
                if (Cycles.Where(c => c.Equals(cycle)).Count() > 1)
                    result.AddErrorMessage(MessageOfDomain.AlreadyHaveItem);

                var resultCycle = cycle.Validate();
                if (resultCycle.IsInvalid)
                    result.AddErrorMessages(resultCycle.ErrorMessagens);
            }

            return result;
        }


        public static Limit Create(LimitType type, FeatureType featureType, bool registrationCompleted = false) =>
           new Limit(type, featureType, registrationCompleted);
    }
}
