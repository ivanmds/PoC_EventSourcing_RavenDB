using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PoC.ES.Api.Domain.Commands.Limits;

namespace PoC.ES.Api.Domain.Handles
{
    public class CustomerHandle : IRequestHandler<CompanyCreate>
    {
        public Task<Unit> Handle(CompanyCreate request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
