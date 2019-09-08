using NSubstitute;
using PoC.ES.Api.Domain.Commands.Limits;
using PoC.ES.Api.Domain.Entities.Limits;
using PoC.ES.Api.Domain.Entities.Limits.Types;
using PoC.ES.Api.Domain.Handles.Limits;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Domain.Services.Limits;
using PoC.ES.Tests.Fixtures;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PoC.ES.Tests.Domain.Handles
{
    public class LimitReserveHandleTest
    {
        private ILimitUsedCommandRepository _repository;
        private ILimitService _limitService;
        private LimitReserveHandle _handle;

        public LimitReserveHandleTest()
        {
            _repository = Substitute.For<ILimitUsedCommandRepository>();
            _limitService = Substitute.For<ILimitService>();
            _handle = new LimitReserveHandle(_repository, _limitService);
        }

        [Fact]
        public async Task ReserveLimitWhenHasLimitAvailable()
        {
            //arrange
            var amountUsed = 3000;
            var limitType = LimitType.CashIn;
            var featureType = FeatureType.TED;
            var cycleType = CycleType.Monthly;
            var levelType = LevelType.Account;

            var limitLevel = LimitLevel.Create(levelType, 80000, 6000);
            var customer = Fixture.CreateLimitCustomer(limitType, featureType, cycleType, limitLevel);
            var command = new ReserveLimitCommand()
            {
                CompanyKey = customer.CompanyKey,
                DocumentNumber = customer.DocumentNumber,
                LimitType = limitType,
                FeatureType = featureType,
                CycleType = cycleType,
                LevelType = levelType,
                Amount = amountUsed
            };

            _limitService.GetLimitAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(customer);

            //act
            var result = await _handle.Handle(command, CancellationToken.None);

            //assert
            await _repository.Received().SaveAsync(Arg.Any<LimitUsed>());
            Assert.True(result.IsValid);
            Assert.Equal(limitLevel.MaxValue - amountUsed, result.Data.LimitAvaliable);
        }

        [Fact]
        public async Task NotReserveLimitWhenHasLimitAvailable()
        {
            //arrange
            var amountUsed = 80000;
            var limitType = LimitType.CashIn;
            var featureType = FeatureType.TED;
            var cycleType = CycleType.Monthly;
            var levelType = LevelType.Account;

            var limitLevel = LimitLevel.Create(levelType, amountUsed - 1, 6000);
            var customer = Fixture.CreateLimitCustomer(limitType, featureType, cycleType, limitLevel);
            var command = new ReserveLimitCommand()
            {
                CompanyKey = customer.CompanyKey,
                DocumentNumber = customer.DocumentNumber,
                LimitType = limitType,
                FeatureType = featureType,
                CycleType = cycleType,
                LevelType = levelType,
                Amount = amountUsed
            };

            _limitService.GetLimitAsync(Arg.Any<string>(), Arg.Any<string>()).Returns(customer);

            //act
            var result = await _handle.Handle(command, CancellationToken.None);

            //assert
            Assert.False(result.IsValid);
            await _repository.DidNotReceive().SaveAsync(Arg.Any<LimitUsed>());
        }
    }
}
