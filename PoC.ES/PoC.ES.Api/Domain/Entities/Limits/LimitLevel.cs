using PoC.ES.Api.Domain.Entities.Limits.Types;
using System;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class LimitLevel : IEquatable<LimitLevel>
    {
        public LimitLevel(LevelType type, long maxValue, long minValue)
        {
            Type = type;
            MaxValue = maxValue;
            MinValue = minValue;
        }

        public LevelType Type { get; private set; }
        public long MaxValue { get; private set; }
        public long MinValue { get; private set; }

        public bool Equals(LimitLevel other)
        {
            return Type == other.Type;
        }

        public static LimitLevel Create(LevelType type, long maxValue, long minValue)
          => new LimitLevel(type, maxValue, minValue);
    }
}
