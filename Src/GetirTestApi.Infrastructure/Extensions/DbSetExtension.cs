using System.Linq;

using Microsoft.EntityFrameworkCore;

using GetirTestApi.Abstractions;
using GetirTestApi.Domain;

namespace GetirTestApi.Infrastructure
{
    internal static class DbSetExtension
    {
        internal static IQueryable<Customer> IncludeQueryAreas(this IQueryable<Customer> customer, QueryAreas queryArea)
        {
            if (queryArea.HasFlag(QueryAreas.Account))
                customer = customer.Include(entity => entity.Accounts);

            if (queryArea.HasFlag(QueryAreas.Transaction))
                customer = customer.Include(entity => entity.Accounts)
                    .ThenInclude(entity => entity.Transactions);

            customer = customer.AsSplitQuery();

            return customer;
        }
    }
}
