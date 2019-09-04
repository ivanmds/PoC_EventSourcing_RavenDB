using PoC.ES.Api.Domain.Entities.Limits.Types;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class LimitLevel
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

        public static LimitLevel Create(LevelType type, long maxValue, long minValue)
            => new LimitLevel(type, maxValue, minValue);
    }
}
