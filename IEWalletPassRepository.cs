namespace Demo.PASS.Faring.Abstractions.Repositories
{
    using System.Threading.Tasks;
    using Demo.Core.DataAccess.Abstraction;
    using Demo.PASS.Faring.Model.Domain;

    /// <summary>
    /// A interface for a service to access the repository for the eWalletPass.
    /// </summary>
    public interface IEWalletPassRepository : IDbRepository<EWalletPass>
    {
        /// <summary>
        /// Adds a EWalletPass to the database.
        /// </summary>
        /// <param name="items">A <see cref="EWalletPass"/> contains the entitie to add.</param>
        /// <returns>A <see cref="Task{int}"/> whose result contains a integer corresponding to the new ezWalletPassId, or 0 if the insert failed.</returns>
        new Task<int> AddAsync(EWalletPass items);
    }
}
