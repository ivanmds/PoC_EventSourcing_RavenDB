using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Limits.Dtos
{
    public class CycleDto
    {
        [JsonConverter(typeof(StringEnumConverter))] public CycleType Type { get; set; }

        public List<LimitLevelDto> LimitLevels { get; set; }
    }
}
