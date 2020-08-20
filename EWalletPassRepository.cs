namespace Demo.PASS.Faring.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Demo.Core.DataAccess.EntityFramework;
    using Demo.PASS.Faring.Abstractions.Repositories;
    using Demo.PASS.Faring.Model.Domain;

    /// <summary>
    /// The implementation for the eWalletPass repository.
    /// </summary>
    /// <typeparam name="TDbContext">A database context of base type <see cref="DemoDbContext"/>.</typeparam>
    public class EWalletPassRepository<TDbContext> : DbRepository<TDbContext, EWalletPass>, IEWalletPassRepository
        where TDbContext : DemoDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EWalletPassRepository{TDbContext}"/> class.
        /// </summary>
        /// <param name="context">A <see cref="TDbContext"/> containing the current db context.</param>
        public EWalletPassRepository(TDbContext context)
            : base(context)
        {
        }

        /// <inheritdoc/>
        public new async Task<int> AddAsync(EWalletPass entityToAdd)
        {
            if (entityToAdd is null)
            {
                return 0;
            }

            entityToAdd.EWalletPassId = await GetNextEZWalletPassId().ConfigureAwait(false);

            if (!await base.AddAsync(entityToAdd).ConfigureAwait(false))
            {
                return 0;
            }

            return entityToAdd.EWalletPassId;
        }

        /// <summary>
        /// The GetNextEZWalletPassId.
        /// </summary>
        /// <returns>The <see cref="Task{int}"/>.</returns>
        private async Task<int> GetNextEZWalletPassId()
        {
            var ezWalletPassId = await DbSet.MaxAsync(ba => (int?)ba.EWalletPassId).ConfigureAwait(false) ?? -1;

            if (ezWalletPassId < 0)
            {
                ezWalletPassId = 0;
            }

            return ++ezWalletPassId;
        }
    }
}
