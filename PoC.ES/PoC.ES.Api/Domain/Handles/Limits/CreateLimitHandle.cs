using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PoC.ES.Api.Domain.Commands.Limits;
using PoC.ES.Api.Results;

namespace PoC.ES.Api.Domain.Handles.Limits
{
    public class CreateLimitHandle : IRequestHandler<CreateLimitCompanyCommand, ResultOfCommand>
    {
        public async Task<ResultOfCommand> Handle(CreateLimitCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
