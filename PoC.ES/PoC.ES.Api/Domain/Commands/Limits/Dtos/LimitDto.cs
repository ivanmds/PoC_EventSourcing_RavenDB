using Raven.Client.Exceptions.Commercial;

namespace PoC.ES.Api.Domain.Commands.Limits.Dtos
{
    public class LimitDto
    {
        public LimitType Type { get; set; }
        public string FeatureType { get; set; }
        public bool RegistrationCompleted { get; set; }
    }
}
