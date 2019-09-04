using PoC.ES.Api.Domain.Types;

namespace PoC.ES.Api.Domain.Entities
{
    public class LimitLevel
    {
        public LevelType Type { get; set; }
        public long MaxValue { get; set; }
        public long MinValue { get; set; }
    }
}
