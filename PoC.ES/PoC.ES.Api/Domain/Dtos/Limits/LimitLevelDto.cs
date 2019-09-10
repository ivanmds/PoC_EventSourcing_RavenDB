using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PoC.ES.Api.Domain.Entities.Limits.Types;

namespace PoC.ES.Api.Domain.Limits.Dtos
{
    public class LimitLevelDto
    {
        [JsonConverter(typeof(StringEnumConverter))] public LevelType Type { get; set; }
        public long MaxValue { get; set; }
        public long MinValue { get; set; }
    }
}
