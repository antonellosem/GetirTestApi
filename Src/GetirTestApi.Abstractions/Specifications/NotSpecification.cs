using System;
using System.Linq;
using System.Linq.Expressions;

namespace GetirTestApi.Abstractions
{
    public class NotSpecification<T> : Specification<T>
        where T : class
    {
        private readonly Specification<T> _item;

        public NotSpecification(Specification<T> item)
        {
            ArgValidator.NotNull(item, nameof(item));

            _item = item;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> itemExpression = _item.ToExpression();

            UnaryExpression expression = Expression.Not(itemExpression.Body); 

            return Expression.Lambda<Func<T, bool>>(
                expression, itemExpression.Parameters.Single());
        }
    }
}
