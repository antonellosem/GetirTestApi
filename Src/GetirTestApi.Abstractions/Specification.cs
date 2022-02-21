using System;
using System.Linq.Expressions;

namespace GetirTestApi.Abstractions
{
    public abstract class Specification<T>
        where T : class
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy (T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public Specification<T> And(Specification<T> specification) => new AndSpecification<T>(this, specification);
        public Specification<T> Or(Specification<T> specification) => new OrSpecification<T>(this, specification);
        public Specification<T> Not(Specification<T> specification) => new NotSpecification<T>(this);
    }
}
