using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Limits.Dtos
{
    public class LimitDto
    {
        [JsonConverter(typeof(StringEnumConverter))] public LimitType Type { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] public FeatureType FeatureType { get; set; }
        public bool RegistrationCompleted { get; set; }

        public List<CycleDto> Cycles { get; set; }
    }
}
