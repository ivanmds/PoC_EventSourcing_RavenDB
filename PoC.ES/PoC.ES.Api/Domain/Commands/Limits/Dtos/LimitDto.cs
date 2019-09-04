using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Commands.Limits.Dtos
{
    public class LimitDto
    {
        public LimitType Type { get; set; }
        public FeatureType FeatureType { get; set; }
        public bool RegistrationCompleted { get; set; }

        public List<CycleDto> Cycles { get; set; }
    }
}
