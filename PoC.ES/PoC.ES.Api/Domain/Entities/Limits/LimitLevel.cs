using PoC.ES.Api.Domain.Types.Limits;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class LimitLevel
    {
        public LevelType Type { get; set; }
        public long MaxValue { get; set; }
        public long MinValue { get; set; }
    }
}
