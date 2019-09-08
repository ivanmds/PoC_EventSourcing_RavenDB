using Microsoft.AspNetCore.Mvc;
using PoC.ES.Api.Domain.Services.Limits;
using System.Threading.Tasks;

namespace PoC.ES.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimitsController : ControllerBase
    {
        private readonly ILimitService _limitService;

        public LimitsController(ILimitService limitService) => _limitService = limitService;


        [HttpGet("{companyKey}/{documentNumber}")]
        public async Task<IActionResult> Get(string companyKey, string documentNumber)
        {
            var limitCustomer = await _limitService.GetLimitAsync(companyKey, documentNumber);

            if (limitCustomer is null) return NotFound();

            return Ok(limitCustomer);
        }
    }
}