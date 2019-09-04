using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PoC.ES.Api.Domain.Commands.Limits;
using PoC.ES.Api.Domain.Commands.Limits.Dtos;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Message;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Results;

namespace PoC.ES.Api.Domain.Handles.Limits
{
    public class CreateLimitHandle : IRequestHandler<CreateLimitCompanyCommand, ResultOfCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public CreateLimitHandle(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ResultOfCommand> Handle(CreateLimitCompanyCommand request, CancellationToken cancellationToken)
        {
            var limitCompany = LimitCompany.Create(request.CompanyKey);
            var codesLimitsParse = LimitsParse(request.Limits);
            var codesAddLimits = limitCompany.AddLimits(codesLimitsParse.Limits);

            var result = limitCompany.Validate();
            result.AddErrorMessages(codesLimitsParse.Codes.Where(p => p.Code != MessageOfDomain.Success.Code));
            result.AddErrorMessages(codesAddLimits.Where(p => p.Code != MessageOfDomain.Success.Code));

            if (result.IsInvalid)
                _companyRepository.AddOrUpdateAsync(limitCompany);

            return result;
        }


        #region HELPER METHODS

        private (IEnumerable<(string Code, string Message)> Codes, IEnumerable<Limit> Limits) LimitsParse(List<LimitDto> limitsDto)
        {
            var listCodes = new List<(string Code, string Message)>();
            var limits = new List<Limit>();

            if (limitsDto.Any())
            {
                foreach (var limitDto in limitsDto)
                {
                    var limit = Limit.Create(limitDto.Type, limitDto.FeatureType, limitDto.RegistrationCompleted);
                    var resultCycle = CyclesParse(limitDto.Cycles);

                    listCodes.AddRange(resultCycle.Codes);

                    var codesAddCycles = limit.AddCycles(resultCycle.Cycles);
                    listCodes.AddRange(codesAddCycles);

                    limits.Add(limit);
                }
            }

            return (listCodes, limits);
        }

        private (IEnumerable<(string Code, string Message)> Codes, IEnumerable<Cycle> Cycles) CyclesParse(List<CycleDto> cyclesDto)
        {
            var listCodes = new List<(string Code, string Message)>();
            var cycles = new List<Cycle>();

            if (cyclesDto.Any())
            {
                foreach (var cycleDto in cyclesDto)
                {
                    var cycle = Cycle.Create(cycleDto.Type);
                    listCodes.AddRange(cycle.AddLimitLevels(LimitLevelsParse(cycleDto.LimitLevels)));
                }
            }

            return (listCodes, cycles);
        }

        private IEnumerable<LimitLevel> LimitLevelsParse(List<LimitLevelDto> limitLevelsDto)
        {
            if (limitLevelsDto.Any())
                foreach (var limitLevelDto in limitLevelsDto)
                    yield return LimitLevel.Create(limitLevelDto.Type, limitLevelDto.MaxValue, limitLevelDto.MinValue);
        }

        #endregion 
    }
}
