namespace Demo.Core.DataAccess.EntityFramework
{
    using Demo.Core.DataAccess.Abstraction;
    using Demo.Core.DataAccess.Abstraction.Paging;
    using Demo.Core.DataAccess.EntityFramework.Extensions;
    using Demo.Core.DataAccess.Models;
    using Demo.Core.DataAccess.Utilities.Criteria;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// A basic read repository.
    /// </summary>
    /// <typeparam name="TDbContext">The context of the database.</typeparam>
    /// <typeparam name="TEntity">The entity to perform read operations on.</typeparam>
    public class QueryRepository<TDbContext, TEntity> : IQueryRepository<TDbContext, TEntity>
        where TDbContext : DemoDbContext
        where TEntity : EntityBase
    {
        /// <summary>
        /// The database context.
        /// </summary>
        protected readonly TDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRepository{TDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="databaseContext">The context information for the repository.</param>
        public QueryRepository(TDbContext databaseContext)
        {
            _dbContext = databaseContext;
        }


        /// <summary>
        /// Gets the database set for the <typeparamref name="TEntity"/>.
        /// </summary>
        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>() as DbSet<TEntity>;

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> FetchByCriteriaAsync(FilterCriteria<TEntity> criteria)
        {
            var query = DbSet
                .AddPredicate(criteria)
                .AddIncludes(criteria)
                .AddPaging(criteria)
                .AddSort(criteria);

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> FetchByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IPaginatedList<TEntity>> GetAllAsync(int maxPageSize = 0)
        {
            return await DbSet.ToPaginatedListAsync(0, 0, maxPageSize).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IPaginatedList<TEntity>> FetchByCriteriaAsync(FilterCriteria<TEntity> criteria, int maxPageSize = 0)
        {
            var query = DbSet
            .AddPredicate(criteria)
            .AddIncludes(criteria)
            .AddSort(criteria);

            return await query.ToPaginatedListAsync(criteria?.Paging?.PageNumber ?? 0, criteria?.Paging?.PageSize ?? 0, maxPageSize).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IPaginatedList<TEntity>> FetchByAsync(Expression<Func<TEntity, bool>> predicate, int maxPageSize = 0)
        {
            return await DbSet.Where(predicate).ToPaginatedListAsync(0, 0, maxPageSize).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByKeyAsync(object key)
        {
            return await DbSet.FindAsync(key);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByKeysAsync(object[] keys)
        {
            return await DbSet.FindAsync(keys);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByCriteriaAsync(FilterCriteria<TEntity> criteria)
        {
            var query = DbSet
                .AddPredicate(criteria)
                .AddIncludes(criteria);

            var result = await query.ToListAsync().ConfigureAwait(false);

            // Find result should only contain single row.
            return result.FirstOrDefault();
        }

        public async Task<TEntity> GetEWalletPassTypeByIdAsync(string spName, SqlParameter[] param)
        {
            TEntity obj=null;

            using (var cnn = _dbContext.Database.GetDbConnection())
            {
                var cmm = cnn.CreateCommand();
                cmm.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmm.CommandText = spName;
                    cmm.Parameters.AddRange(param);
                    cmm.Connection = cnn;
                    cnn.Open();
                    var reader = await cmm.ExecuteReaderAsync().ConfigureAwait(false);
                    
                    if (await reader.ReadAsync().ConfigureAwait(false))
                    {

                        // mappedObject = new SqlDataReaderMapper<TEntity>(reader)
                        //.Build();

                        Type type = typeof(TEntity);
                         obj = (TEntity)Activator.CreateInstance(type);
                        PropertyInfo[] properties = type.GetProperties();

                        foreach (PropertyInfo property in properties)
                        {
                            try
                            {
                                var value = reader[property.Name];
                                if (value != null)
                                    property.SetValue(obj, Convert.ChangeType(value.ToString(), property.PropertyType));
                            }
                            catch { }
                        }
                        //result.Add(obj);
                       
                    }

                    return obj;

                }
                catch (Exception ex)
                {
                    throw ex;

                }
                finally
                {
                    cmm.Parameters.Clear();
                    cmm.Dispose();
                }
            }
        }
    }
}