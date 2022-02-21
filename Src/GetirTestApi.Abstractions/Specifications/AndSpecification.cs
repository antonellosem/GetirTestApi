using System;
using System.Linq;
using System.Linq.Expressions;

namespace GetirTestApi.Abstractions
{
    public class AndSpecification<T> : Specification<T>
        where T : class
    {
        private readonly Specification<T> _right;
        private readonly Specification<T> _left;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            ArgValidator.NotNull(left, nameof(left));
            ArgValidator.NotNull(right, nameof(right));

            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            Expression<Func<T, bool>> leftExpression = _left.ToExpression();
            Expression<Func<T, bool>> rightExpression = _right.ToExpression();

            BinaryExpression expression = Expression.AndAlso(
                leftExpression.Body, rightExpression.Body); 

            return Expression.Lambda<Func<T, bool>>(
                expression, leftExpression.Parameters.Single());
        }
    }
}
