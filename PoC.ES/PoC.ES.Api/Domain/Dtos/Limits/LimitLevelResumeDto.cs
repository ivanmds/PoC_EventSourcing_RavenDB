using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PoC.ES.Api.Domain.Entities.Limits.Types;

namespace PoC.ES.Api.Domain.Dtos.Limits
{
    public class LimitLevelResumeDto
    {
        public string CompanyKey { get; set; }
        public string DocumentNumber { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] public LimitType LimitType { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] public FeatureType FeatureType { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] public CycleType CycleType { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] public LevelType LevelType { get; set; }
        public long Amount { get; set; }
    }
}
