using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Limits.Dtos
{
    public class LimitConfigurationDto
    {
        public string CompanyKey { get; set; }
        public List<LimitDto> Limits { get; set; }
    }
}
