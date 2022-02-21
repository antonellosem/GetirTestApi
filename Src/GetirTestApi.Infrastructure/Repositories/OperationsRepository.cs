using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using GetirTestApi.Abstractions;
using GetirTestApi.Domain;

namespace GetirTestApi.Infrastructure
{
    public class OperationsRepository : IOperationsRepository
    {
        private readonly OperationsContext _context;

        public OperationsRepository(OperationsContext context)
        {
            ArgValidator.NotNull(context, nameof(context));

            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Customer> Create(Customer entity)
        {
            var entry = await _context.Customers.AddAsync(entity);

            return entry.Entity;
        }

        public async Task<Customer> Read(Guid id)
        {
            return await Find(new CustomerByIdSpecification(id));
        }

        public void Update(Customer entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Customer entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<Customer> Find(Specification<Customer> specification, QueryAreas queryArea = QueryAreas.Root)
        {
            return await _context.Customers
                .Where(specification.ToExpression())
                .IncludeQueryAreas(queryArea)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> FindTransactions(Specification<Customer> customerSpecification, Specification<Transaction> transactionSpecification)
        {
            return await _context.Customers
                .Where(customerSpecification.ToExpression())
                .SelectMany(c => c.Accounts)
                .SelectMany(a => a.Transactions)
                .Where(transactionSpecification.ToExpression())
                .ToListAsync();
        }
    }
}
