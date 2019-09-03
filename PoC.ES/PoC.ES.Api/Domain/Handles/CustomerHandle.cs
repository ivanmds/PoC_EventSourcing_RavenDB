using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PoC.ES.Api.Domain.Commands;

namespace PoC.ES.Api.Domain.Handles
{
    public class CustomerHandle : IRequestHandler<CustomerCreate>
    {
        public Task<Unit> Handle(CustomerCreate request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
