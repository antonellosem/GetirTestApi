using System;
using System.Linq.Expressions;

using GetirTestApi.Abstractions;
using GetirTestApi.Domain;

namespace GetirTestApi.Infrastructure
{
    public class TransactionsByDateRangeSpecification : Specification<Transaction>
    {
        private readonly DateTime _from;
        private readonly DateTime _to;

        public TransactionsByDateRangeSpecification(DateTime from, DateTime to)
        {
            ArgValidator.NotNull(from, nameof(from));
            ArgValidator.NotNull(to, nameof(to));

            _from = from;
            _to = to;
        }

        public override Expression<Func<Transaction, bool>> ToExpression()
        {
            return transaction => transaction.CreatedOn >= _from && transaction.CreatedOn <= _to;
        }
    }
}
