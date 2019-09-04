using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Message;
using Xunit;

namespace PoC.ES.Tests.Domain.Entities
{
    public class LimitCompanyTest
    {
        [Fact]
        public void LimitCompanyHaveAddNewLimit()
        {
            //arrange
            var limit = Limit.Create(LimitType.CashIn, FeatureType.TED);
            var limitCompany = LimitCompany.Create("123456");
            limitCompany.AddLimit(limit);

            //act
            var result = limitCompany.Validate();

            //assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void LimitCompanyDontHaveTwoLimitEquals()
        {
            //arrange
            var limit = Limit.Create(LimitType.CashIn, FeatureType.TED);
            var limitDouble = Limit.Create(LimitType.CashIn, FeatureType.TED);

            var limitCompany = LimitCompany.Create("123456");
            limitCompany.AddLimit(limit);

            //act
            limitCompany.AddLimit(limitDouble);
            var result = limitCompany.Validate();

            //assert
            Assert.True(result.IsInvalid);
        }
    }
}
