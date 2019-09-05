using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Limits.Dtos
{
    public class CycleDto
    {
        public CycleType Type { get; set; }

        public List<LimitLevelDto> LimitLevels { get; set; }
    }
}
