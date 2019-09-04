using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Cycle
    {
        public Cycle(CycleType type)
        {
            Type = type;
        }

        public CycleType Type { get; private set; }

        private List<LimitLevel> _limitLevels = new List<LimitLevel>();
        public IReadOnlyCollection<LimitLevel> LimitLevels
        {
            get => _limitLevels;
            private set => _limitLevels.AddRange(value);
        }

        public void AddLimitLevel(LimitLevel limitLevel) => _limitLevels.Add(limitLevel);

        public static Cycle Create(CycleType type) => new Cycle(type);
    }
}
