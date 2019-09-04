using PoC.ES.Api.Domain.Entities.Limits.Types;

namespace PoC.ES.Api.Domain.Commands.Limits.Dtos
{
    public class LimitLevelDto
    {
        public LevelType Type { get; set; }
        public long MaxValue { get; set; }
        public long MinValue { get; set; }
    }
}
