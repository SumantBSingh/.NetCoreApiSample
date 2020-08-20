namespace Demo.Core.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Demo.Core.DataAccess.Abstraction;
    using Demo.Core.DataAccess.Models;

    /// <summary>
    /// A basic CRUD repository.
    /// </summary>
    /// <typeparam name="TDbContext">The context of the database.</typeparam>
    /// <typeparam name="TEntity">The entity to perform CRUD operations on.</typeparam>
    public class DbRepository<TDbContext, TEntity> : QueryRepository<TDbContext, TEntity>, IDbRepository<TDbContext, TEntity>
        where TDbContext : DemoDbContext
        where TEntity : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbRepository{TDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="databaseContext">The context information for the repository.</param>
        public DbRepository(TDbContext databaseContext)
            : base(databaseContext)
        {
        }

        /// <inheritdoc/>
        public async Task<bool> AddAsync(TEntity item)
        {
            int result = 0;
            await DbSet.AddAsync(item).ConfigureAwait(false);
            result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        /// <inheritdoc/>
        public async Task<bool> AddAsync(IEnumerable<TEntity> items)
        {
            int result = 0;
            await DbSet.AddRangeAsync(items).ConfigureAwait(false);
            result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsync(TEntity item)
        {
            int result = 0;
            _dbContext.SetValues(item);
            result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsync(IEnumerable<TEntity> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            int result = 0;

            foreach (TEntity item in items)
            {
                _dbContext.SetValues(item);
            }

            result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(TEntity item)
        {
            int result = 0;
            DbSet.Remove(ForDelete(item));
            result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(IEnumerable<TEntity> items)
        {
            int result = 0;
            DbSet.RemoveRange(ForDelete(items));
            result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return result > 0;
        }

        /// <summary>
        /// Returns a <see cref="DbSet{TRelatedEntity}"/> for an entity related to entity.
        /// </summary>
        /// <typeparam name="TRelatedEntity">A related entity type.</typeparam>
        /// <returns>The <see cref="DbSet{TRelatedEntity}"/>.</returns>
        protected virtual DbSet<TRelatedEntity> RelatedEntityDbSet<TRelatedEntity>()
            where TRelatedEntity : class
        {
            return _dbContext.Set<TRelatedEntity>();
        }

        private TEntity ForDelete(TEntity entity)
        {
            // Find the underlying entity. This could be a tracked version of the entity, or a newlyy created entity.
            return _dbContext.TrackItem(entity);
        }

        private TEntity[] ForDelete(IEnumerable<TEntity> entities)
        {
            return entities.Select(ForDelete).ToArray();
        }
    }
}