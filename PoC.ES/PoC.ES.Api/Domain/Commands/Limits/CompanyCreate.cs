using MediatR;

namespace PoC.ES.Api.Domain.Commands.Limits
{
    public class CompanyCreate : IRequest
    {
        public string CompanyKey { get; set; }
     
    }
}
