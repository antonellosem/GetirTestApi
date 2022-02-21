using System;
using System.Linq.Expressions;

using GetirTestApi.Abstractions;
using GetirTestApi.Domain;

namespace GetirTestApi.Infrastructure
{
    public class CustomerByNationalIdSpecification : Specification<Customer>
    {
        private readonly string _nationalId;

        public CustomerByNationalIdSpecification(string nationalId)
        {
            ArgValidator.NotNull(nationalId, nameof(nationalId));

            _nationalId = nationalId.Trim();
        }

        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return Customer => Customer.NationalId.Equals(_nationalId);
        }
    }
}
