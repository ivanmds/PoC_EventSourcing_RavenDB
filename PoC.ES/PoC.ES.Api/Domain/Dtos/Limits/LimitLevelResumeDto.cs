using PoC.ES.Api.Domain.Entities.Limits.Types;

namespace PoC.ES.Api.Domain.Dtos.Limits
{
    public class LimitLevelResumeDto
    {
        public string CompanyKey { get; set; }
        public string DocumentNumber { get; set; }
        public LimitType LimitType { get; set; }
        public FeatureType FeatureType { get; set; }
        public CycleType CycleType { get; set; }
        public LevelType LevelType { get; set; }
        public long Amount { get; set; }
    }
}
