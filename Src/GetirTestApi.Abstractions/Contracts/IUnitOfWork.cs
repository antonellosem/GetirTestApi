using System.Threading.Tasks;

namespace GetirTestApi.Abstractions
{
    /// <summary>
    /// Specifies the contract of the "Unit of Work" pattern
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all uncommit changes in the current unit of work.
        /// </summary>
        /// <returns>An integer value indicating the saving status result.</returns>
        Task<int> Commit();
        
        /// <summary>
        /// Undos all uncommit changes in the current unit of work.
        /// </summary>
        /// <returns></returns>
        Task Rollback();
    }
}
