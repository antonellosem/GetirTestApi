using System;
using System.Linq;
using System.Linq.Expressions;

using GetirTestApi.Abstractions;
using GetirTestApi.Domain;

namespace GetirTestApi.Infrastructure
{
    public class CustomerByAccountIdSpecification : Specification<Customer>
    {
        private readonly Guid _accountId;

        public CustomerByAccountIdSpecification(Guid accountId)
        {
            ArgValidator.NotNull(accountId, nameof(accountId));

            _accountId = accountId;
        }

        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return Customer => Customer.Accounts.Any(x => x.Id.Equals(_accountId));
        }
    }
}
