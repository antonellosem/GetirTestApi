using System;
using System.Linq.Expressions;

using GetirTestApi.Abstractions;
using GetirTestApi.Domain;

namespace GetirTestApi.Infrastructure
{
    public class CustomerByIdSpecification : Specification<Customer>
    {
        private readonly Guid _customerId;

        public CustomerByIdSpecification(Guid customerId)
        {
            ArgValidator.NotNull(customerId, nameof(customerId));

            _customerId = customerId;
        }

        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return Customer => Customer.Id.Equals(_customerId);
        }
    }
}
