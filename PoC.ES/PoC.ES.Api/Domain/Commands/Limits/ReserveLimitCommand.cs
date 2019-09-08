using MediatR;
using PoC.ES.Api.Domain.Dtos.Limits;
using PoC.ES.Api.Results;

namespace PoC.ES.Api.Domain.Commands.Limits
{
    public class ReserveLimitCommand : LimitLevelResumeDto, IRequest<ResultOfCommandData<LimitAvaliableDto>>
    {
    }
}
