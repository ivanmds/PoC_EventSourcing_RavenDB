using PoC.ES.Api.Domain.Limits.Dtos;
using System;
using System.Collections.Generic;

namespace PoC.ES.Api.Domain.Queries.Limits
{
    public class LimitCustomerForDailyQuery
    {
        public LimitCustomerForDailyQuery(string companyKey, string documentNumber, DateTime day)
        {
            CompanyKey = companyKey;
            DocumentNumber = documentNumber;
            Day = day;
        }

        public string CompanyKey { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime Day { get; set; }

        public List<LimitLevelDto> DataResult { get; set; }

        public static LimitCustomerForDailyQuery Create(string companyKey, string documentNumber, DateTime day)
            => new LimitCustomerForDailyQuery(companyKey, documentNumber, day);
    }
}
