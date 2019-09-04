using PoC.ES.Api.Domain.Types.Limits;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Cycle
    {
        public CycleType Type { get; private set; }

        private List<LimitLevel> _limitLevels = new List<LimitLevel>();
        public IReadOnlyCollection<LimitLevel> LimitLevels
        {
            get => _limitLevels;
            set
            {
                _limitLevels.AddRange(value);
            }
        }
    }
}
