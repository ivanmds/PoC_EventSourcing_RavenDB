using MediatR;
using Microsoft.AspNetCore.Mvc;
using PoC.ES.Api.Domain.Commands.Limits;
using PoC.ES.Api.Domain.Services.Limits;
using System.Threading.Tasks;

namespace PoC.ES.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimitsController : ControllerBase
    {
        private readonly ILimitService _limitService;
        private readonly IMediator _mediator;

        public LimitsController(ILimitService limitService, IMediator mediator)
        {
            _limitService = limitService;
            _mediator = mediator;
        }


        [HttpGet("{companyKey}/{documentNumber}")]
        public async Task<IActionResult> Get(string companyKey, string documentNumber)
        {
            var limitCustomer = await _limitService.GetLimitAsync(companyKey, documentNumber);

            if (limitCustomer is null) return NotFound();

            return Ok(limitCustomer);
        }

        [HttpPut("company")]
        public async Task<IActionResult> Put([FromBody] CreateLimitCompanyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPut("reserveLimit")]
        public async Task<IActionResult> ReserveLimit([FromBody] ReserveLimitCommand command)
        {
            var result =  await _mediator.Send(command);
            return Ok(result);
        }
    }
}