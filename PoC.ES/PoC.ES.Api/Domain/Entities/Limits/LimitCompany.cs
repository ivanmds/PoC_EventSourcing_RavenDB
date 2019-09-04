using PoC.ES.Api.Domain.Message;
using PoC.ES.Api.Domain.Validation;
using PoC.ES.Api.Results;
using System.Linq;

namespace PoC.ES.Api.Domain.Entities.Limits
{
    public class LimitCompany : LimitConfiguration, IValidated
    {
        public LimitCompany(string companyKey) : base(companyKey) => Id = companyKey;

        public static LimitCompany Create(string companyKey) => new LimitCompany(companyKey);

        public ResultOfCommand Validate()
        {
            var result = ResultOfCommand.Create();

            if (Limits.Any())
            {
                foreach (var limit in Limits)
                {
                    if (Limits.Where(l => l.Equals(limit)).Count() > 1)
                        result.AddErrorMessage(MessageOfDomain.AlreadyHaveItem);

                    var limitResult = limit.Validate();
                    if (limitResult.IsInvalid)
                        result.AddErrorMessages(limitResult.ErrorMessagens);
                }
            }
            else
                result.AddErrorMessage(MessageOfDomain.InvalidItem);

            return result;
        }
    }
}
