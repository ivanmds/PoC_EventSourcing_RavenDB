using NSubstitute;
using PoC.ES.Api.Domain.Dtos.Limits;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Domain.Services.Limits;
using PoC.ES.Tests.Fixtures;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PoC.ES.Tests.Domain.Services
{
    public class LimitServiceTest
    {
        private ICompanyQueryRepository _companyRepositoryQuery;
        private ICustomerQueryRepository _customerRepositoryQuery;
        private ILimitUsedQueryRepository _limitUsedQueryRepository;
        private ILimitService _limitService;

        public LimitServiceTest()
        {
            _companyRepositoryQuery = Substitute.For<ICompanyQueryRepository>();
            _customerRepositoryQuery = Substitute.For<ICustomerQueryRepository>();
            _limitUsedQueryRepository = Substitute.For<ILimitUsedQueryRepository>();
            _limitService = new LimitService(_companyRepositoryQuery, _customerRepositoryQuery, _limitUsedQueryRepository);
        }


        [Fact]
        public async Task GetLimit()
        {
            //arrange
            var company = Fixture.GetLimitCompanyValid();
            _companyRepositoryQuery.GetAsync(Arg.Any<string>()).Returns(company);
            _customerRepositoryQuery.GetAsync(Arg.Any<string>()).Returns((LimitCustomer)null);

            //act
            var limitCustomer = await _limitService.GetLimitAsync("ACESSO", "123456");

            //assert
            Assert.NotNull(limitCustomer);
        }

        [Fact]
        public async Task GetLimitJoinWithLimitCustomer()
        {
            //arrange
            var company = Fixture.GetLimitCompanyValid();
            var documentNumber = "document123";
            var limitType = LimitType.CashIn;
            var featureType = FeatureType.TED;
            var cycleType = CycleType.Monthly;
            var levelType = LevelType.Account;

            var limitLevel = LimitLevel.Create(levelType, 80000, 6000);
            var customer = Fixture.CreateLimitCustomer(limitType, featureType, cycleType, limitLevel);

            _companyRepositoryQuery.GetAsync(Arg.Any<string>()).Returns(company);
            _customerRepositoryQuery.GetAsync(Arg.Any<string>()).Returns(customer);

            //act
            var limitCustomer = await _limitService.GetLimitAsync(company.CompanyKey, documentNumber);
            var limitLevelGet = limitCustomer.GetLimitLevel(limitType, featureType, cycleType, levelType);

            //assert
            Assert.NotNull(limitLevelGet);
            Assert.Equal(limitLevel.Type, limitLevelGet.Type);
            Assert.Equal(limitLevel.MaxValue, limitLevelGet.MaxValue);
            Assert.Equal(limitLevel.MinValue, limitLevelGet.MinValue);
        }

        [Fact]
        public async Task GetLimitJoinWithLimitCustomerWithLimitUsed()
        {
            //arrange
            var company = Fixture.GetLimitCompanyValid();
            var documentNumber = "document123";
            var limitType = LimitType.CashIn;
            var featureType = FeatureType.TED;
            var cycleType = CycleType.Monthly;
            var levelType = LevelType.Account;
            var limitUsedMax = 600;
            var limitMaxValue = 80000;

            var limitLevel = LimitLevel.Create(levelType, limitMaxValue, 6000);
            var customer = Fixture.CreateLimitCustomer(limitType, featureType, cycleType, limitLevel);

            var limitUsed = new LimitLevelResumeDto() { CompanyKey = company.CompanyKey, DocumentNumber = documentNumber, LimitType = limitType, FeatureType = featureType, CycleType = cycleType, LevelType = levelType, Amount = limitUsedMax };
            var listLimitUsed = new List<LimitLevelResumeDto> { limitUsed };

            _companyRepositoryQuery.GetAsync(Arg.Any<string>()).Returns(company);
            _customerRepositoryQuery.GetAsync(Arg.Any<string>()).Returns(customer);
            _limitUsedQueryRepository.GetResumeAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(listLimitUsed);

            //act
            var limitCustomer = await _limitService.GetLimitAsync(company.CompanyKey, documentNumber);
            var limitLevelGet = limitCustomer.GetLimitLevel(limitType, featureType, cycleType, levelType);

            //assert
            Assert.NotNull(limitLevelGet);
            Assert.Equal(limitLevel.Type, limitLevelGet.Type);
            Assert.Equal(limitMaxValue - limitUsedMax, limitLevelGet.MaxValue);
            Assert.Equal(limitLevel.MinValue, limitLevelGet.MinValue);
        }
    }
}
