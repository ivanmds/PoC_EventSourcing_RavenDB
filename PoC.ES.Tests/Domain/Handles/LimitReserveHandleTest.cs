using NSubstitute;
using PoC.ES.Api.Domain.Repositories.Limits;
using PoC.ES.Api.Domain.Services.Limits;
using System.Threading.Tasks;

namespace PoC.ES.Tests.Domain.Handles
{
    public class LimitReserveHandleTest
    {
        private ILimitUsedCommandRepository _limitUsedCommandRepository;
        private ILimitService _limitService;

        public LimitReserveHandleTest()
        {
            _limitUsedCommandRepository = Substitute.For<ILimitUsedCommandRepository>();
            _limitService = Substitute.For<ILimitService>();
        }

        public async Task ReserveLimitWhenHasLimitAvailable()
        {
            //arrange


            //act

            //assert
        }
    }
}
