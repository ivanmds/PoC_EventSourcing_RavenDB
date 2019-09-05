using MediatR;
using PoC.ES.Api.Domain.Limits.Dtos;
using PoC.ES.Api.Results;

namespace PoC.ES.Api.Domain.Commands.Limits
{
    public class CreateLimitCompanyCommand: LimitCompanyDto, IRequest<ResultOfCommand>
    {
    }
}
