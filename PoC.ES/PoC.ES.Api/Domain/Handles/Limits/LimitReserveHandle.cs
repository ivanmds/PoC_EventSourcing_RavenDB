using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PoC.ES.Api.Domain.Commands.Limits;
using PoC.ES.Api.Domain.Message;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Domain.Services.Limits;
using PoC.ES.Api.Results;

namespace PoC.ES.Api.Domain.Handles.Limits
{
    public class LimitReserveHandle : IRequestHandler<ReserveLimitCommand, ResultOfCommand>
    {
        private readonly ILimitUsedCommandRepository _limitUsedCommand;
        private readonly ILimitService _limitService;

        public LimitReserveHandle(ILimitUsedCommandRepository limitUsedCommand, ILimitService limitService)
        {
            _limitUsedCommand = limitUsedCommand;
            _limitService = limitService;
        }

        public async Task<ResultOfCommand> Handle(ReserveLimitCommand request, CancellationToken cancellationToken)
        {
            var result = ResultOfCommand.Create();
            var limitCustomer = await _limitService.GetLimitAsync(request.CompanyKey, request.DocumentNumber);

            var limitLevel = limitCustomer.GetLimitLevel(request.LimitType, request.FeatureType, request.CycleType, request.LevelType);

            if (limitLevel.MaxValue > request.Amount)
            {

            }
            else
                result.AddErrorMessage(MessageOfDomain.DontHaveLimit);



            return result;
        }
    }
}
