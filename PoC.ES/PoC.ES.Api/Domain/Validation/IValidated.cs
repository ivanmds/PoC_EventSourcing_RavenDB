using PoC.ES.Api.Results;

namespace PoC.ES.Api.Domain.Validation
{
    public interface IValidated
    {
        ResultOfCommand Validate();
    }
}
