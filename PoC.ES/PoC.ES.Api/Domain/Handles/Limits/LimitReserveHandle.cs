using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PoC.ES.Api.Domain.Commands.Limits;
using PoC.ES.Api.Domain.Dtos.Limits;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Message;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Domain.Services.Limits;
using PoC.ES.Api.Results;

namespace PoC.ES.Api.Domain.Handles.Limits
{
    public class LimitReserveHandle : IRequestHandler<ReserveLimitCommand, ResultOfCommandData<LimitAvaliableDto>>
    {
        private readonly ILimitUsedCommandRepository _limitUsedCommand;
        private readonly ILimitService _limitService;

        public LimitReserveHandle(ILimitUsedCommandRepository limitUsedCommand, ILimitService limitService)
        {
            _limitUsedCommand = limitUsedCommand;
            _limitService = limitService;
        }

        public async Task<ResultOfCommandData<LimitAvaliableDto>> Handle(ReserveLimitCommand request, CancellationToken cancellationToken)
        {
            var result = new ResultOfCommandData<LimitAvaliableDto>();
            var limitCustomer = await _limitService.GetLimitAsync(request.CompanyKey, request.DocumentNumber);

            var limitLevel = limitCustomer.GetLimitLevel(request.LimitType, request.FeatureType, request.CycleType, request.LevelType);

            if (limitLevel.MaxValue > request.Amount)
            {
                var limitUsed = LimitUsed.Create(request.CompanyKey,
                                                 request.DocumentNumber,
                                                 request.LimitType,
                                                 request.FeatureType,
                                                 request.CycleType,
                                                 request.LevelType,
                                                 request.Amount);

                await _limitUsedCommand.SaveAsync(limitUsed);
                result.Data = new LimitAvaliableDto { AmountUsed = request.Amount, LimitAvaliable = limitLevel.MaxValue - request.Amount };
            }
            else
                result.AddErrorMessage(MessageOfDomain.DontHaveLimit);

            return result;
        }
    }
}
