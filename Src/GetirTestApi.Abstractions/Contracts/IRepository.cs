using System;
using System.Threading.Tasks;

namespace GetirTestApi.Abstractions
{
    /// <summary>
    /// Specifies the contract of a generic repository with CRUD operations
    /// </summary>
    /// <typeparam name="T">The type <seealso cref="IRootAggregate"/> entity type.</typeparam>
    public interface IRepository<T>
        where T : IRootAggregate
    {
        /// <summary>
        /// Gets the repository unit of work
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        Task<T> Create(T entity);
        Task<T> Read(Guid id);
        void Update(T entity);
        void Delete(T entity);
    }
}
