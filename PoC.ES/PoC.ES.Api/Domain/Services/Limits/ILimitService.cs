using PoC.ES.Api.Domain.Entities.Limits;
using System.Threading.Tasks;

namespace PoC.ES.Api.Domain.Services.Limits
{
    public interface ILimitService
    {
        Task<LimitCustomer> GetLimitAsync(string companyKey, string documentNumber);
    }
}
