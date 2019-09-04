using System;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Collections.Generic;
using System.Linq;
using PoC.ES.Api.Domain.Message;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class Cycle : IEquatable<Cycle>
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

        public (string Code, string Message) AddLimitLevel(LimitLevel limitLevel)
        {
            var alreadyHave = _limitLevels.Any(l => l.Equals(limitLevel));
            if (alreadyHave) return MessageOfDomain.AlreadyItem;

            _limitLevels.Add(limitLevel);
            return MessageOfDomain.Success;
        }

        public bool Equals(Cycle other)
        {
            return Type == other.Type;
        }

        public static Cycle Create(CycleType type) => new Cycle(type);
    }
}
