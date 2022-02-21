using System.Collections.Generic;
using System.Threading.Tasks;

using GetirTestApi.Abstractions;
using GetirTestApi.Domain;

namespace GetirTestApi.Infrastructure
{
    public interface IOperationsRepository : IRepository<Customer>
    {
        Task<Customer> Find(Specification<Customer> specification, QueryAreas queryArea = QueryAreas.Root);
        Task<IEnumerable<Transaction>> FindTransactions(Specification<Customer> customerSpecification, Specification<Transaction> transactionSpecification);
    }
}
