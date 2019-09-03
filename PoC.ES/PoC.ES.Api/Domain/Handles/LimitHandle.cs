using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PoC.ES.Api.Domain.Commands;

namespace PoC.ES.Api.Domain.Handles
{
    public class LimitHandle : IRequestHandler<LimitCreate>
    {
        public Task<Unit> Handle(LimitCreate request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
