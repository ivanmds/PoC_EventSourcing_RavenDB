using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PoC.ES.Api.Domain.Commands.Limits;
using PoC.ES.Api.Domain.Commands.Limits.Dtos;
using PoC.ES.Api.Domain.Entities.Limits;
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
            var limits = LimitsParse(request.Limits);
            limitCompany.AddLimits(limits);
            
            var result = limitCompany.Validate();

            if (result.IsValid)
                await _companyRepository.SaveAsync(limitCompany);
            else
                result.Data = request;

            return result;
        }


        #region HELPER METHODS

        private IEnumerable<Limit> LimitsParse(List<LimitDto> limitsDto)
        {
            if (limitsDto?.Any() == true)
            {
                foreach (var limitDto in limitsDto)
                {
                    var limit = Limit.Create(limitDto.Type, limitDto.FeatureType, limitDto.RegistrationCompleted);
                    var cycles = CyclesParse(limitDto.Cycles);

                    limit.AddCycles(cycles);

                    yield return limit;
                }
            }
        }

        private IEnumerable<Cycle> CyclesParse(List<CycleDto> cyclesDto)
        {
            if (cyclesDto?.Any() == true)
            {
                foreach (var cycleDto in cyclesDto)
                {
                    var cycle = Cycle.Create(cycleDto.Type);
                    cycle.AddLimitLevels(LimitLevelsParse(cycleDto.LimitLevels));
                    yield return cycle;
                }
            }
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
