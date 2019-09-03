using MediatR;

namespace PoC.ES.Api.Domain.Commands
{
    public class CustomerCreate : IRequest
    {
        public string Type { get; set; }
        public int Amount { get; set; }
    }
}
