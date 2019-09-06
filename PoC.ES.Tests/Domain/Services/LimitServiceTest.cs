using NSubstitute;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Domain.Services.Limits;
using PoC.ES.Tests.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace PoC.ES.Tests.Domain.Services
{
    public class LimitServiceTest
    {
        private ICompanyQueryRepository _companyRepositoryQuery;
        private ICustomerQueryRepository _customerRepositoryQuery;
        private ILimitService _limitService;

        public LimitServiceTest()
        {
            _companyRepositoryQuery = Substitute.For<ICompanyQueryRepository>();
            _customerRepositoryQuery = Substitute.For<ICustomerQueryRepository>();
            _limitService = new LimitService(_companyRepositoryQuery, _customerRepositoryQuery);
        }


        [Fact]
        public async Task GetLimit()
        {
            //arrange
            var company = LimitCompanyTest.GetLimitCompanyValid();
            _companyRepositoryQuery.GetAsync(Arg.Any<string>()).Returns(company);
            _customerRepositoryQuery.GetAsync(Arg.Any<string>()).Returns((LimitCustomer)null);

            //act
            var limitCustomer = await _limitService.GetLimitAsync("ACESSO", "123456");

            //assert
            Assert.NotNull(limitCustomer);
        }
    }
}
