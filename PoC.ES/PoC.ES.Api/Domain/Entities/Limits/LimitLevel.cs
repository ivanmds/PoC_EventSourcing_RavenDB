using PoC.ES.Api.Domain.Types.Limits;

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

        public LevelType Type { get; set; }
        public long MaxValue { get; set; }
        public long MinValue { get; set; }

        public static LimitLevel Create(LevelType type, long maxValue, long minValue)
            => new LimitLevel(type, maxValue, minValue);
    }
}
