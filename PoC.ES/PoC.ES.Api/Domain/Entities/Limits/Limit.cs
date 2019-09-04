using PoC.ES.Api.Domain.Types.Limits;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Limit
    {
        public LimitType Type { get; private set; }
        public string FeatureType { get; private set; }
        public bool RegistrationCompleted { get; private set; }

        private List<Cycle> _cycles = new List<Cycle>();
        public IReadOnlyCollection<Cycle> Cycles
        {
            get => _cycles;
            set
            {
                _cycles.AddRange(value);
            }
        }

        public void AddCycle(Cycle cycle)
        {
            _cycles.Add(cycle);
        }
    }
}
