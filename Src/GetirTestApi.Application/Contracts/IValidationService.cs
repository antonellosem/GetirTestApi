using System;
using System.Threading.Tasks;

namespace GetirTestApi.Application
{
    public interface IValidationService
    {
        Task<bool> IsValidCustomerId(Guid customerId);
        Task<bool> IsValidAccountId(Guid accountId);
        Task<bool> IsValidNationalId(string nationalId);

        bool IsValidIBAN(string iban);
        bool IsValidAmountRange(decimal amount);
        bool IsValidAmountScale(decimal amount);
    }
}
