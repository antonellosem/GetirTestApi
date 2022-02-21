using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using GetirTestApi.Infrastructure;

namespace GetirTestApi.Application
{
    internal class ValidationService : IValidationService
    {
        private readonly IOperationsRepository _repository;
    
        public ValidationService(IOperationsRepository repository)
        {
            ArgValidator.NotNull(repository, nameof(repository));

            _repository = repository;
        }

        public async Task<bool> IsValidCustomerId(Guid customerId)
        {
            var existingCustomer = await _repository.Find(new CustomerByIdSpecification(customerId));

            return existingCustomer != null;
        }

        public async Task<bool> IsValidAccountId(Guid accountId)
        {
            var existingCustomer = await _repository.Find(new CustomerByAccountIdSpecification(accountId));

            return existingCustomer != null;
        }

        public async Task<bool> IsValidNationalId(string nationalId)
        {
            var existingCustomer = await _repository.Find(new CustomerByNationalIdSpecification(nationalId));

            return existingCustomer == null;
        }

        public bool IsValidIBAN(string iban) => Regex.Match(iban, IBAN_Regex).Success;

        public bool IsValidAmountRange(decimal amount) => amount > RANGE_MIN && amount < RANGE_MAX;
        public bool IsValidAmountScale(decimal amount) => HasMaxScale(amount, 2);

        private static bool HasMaxScale(decimal value, int scale)
        {
            decimal dec = value * (int)Math.Pow(10, scale);
            return dec == Math.Floor(dec);
        }

        private const string IBAN_Regex = "^(?:(?:IT|SM)\\d{2}[A-Z]\\d{22}|CY\\d{2}[A-Z]\\d{23}|NL\\d{2}[A-Z]{4}\\d{10}|LV\\d{2}[A-Z]{4}\\d{13}|(?:BG|BH|GB|IE)\\d{2}[A-Z]{4}\\d{14}|GI\\d{2}[A-Z]{4}\\d{15}|RO\\d{2}[A-Z]{4}\\d{16}|KW\\d{2}[A-Z]{4}\\d{22}|MT\\d{2}[A-Z]{4}\\d{23}|NO\\d{13}|(?:DK|FI|GL|FO)\\d{16}|MK\\d{17}|(?:AT|EE|KZ|LU|XK)\\d{18}|(?:BA|HR|LI|CH|CR)\\d{19}|(?:GE|DE|LT|ME|RS)\\d{20}|IL\\d{21}|(?:AD|CZ|ES|MD|SA)\\d{22}|PT\\d{23}|(?:BE|IS)\\d{24}|(?:FR|MR|MC)\\d{25}|(?:AL|DO|LB|PL)\\d{26}|(?:AZ|HU)\\d{27}|(?:GR|MU)\\d{28})$";

        private const decimal RANGE_MIN = 0m;
        private const decimal RANGE_MAX = 1_000_000m;
    }
}
