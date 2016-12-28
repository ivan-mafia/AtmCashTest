// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the Repository type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Data.DbContext
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// The repository.
    /// </summary>
    public class Repository
    {
        #region Public Methods
        /// <summary>
        /// Loads data from context.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// Selected entity.
        /// </typeparam>
        /// <returns>
        /// The <see cref="IQueryable"/>.
        /// </returns>
        public static IQueryable<TEntity> Select<TEntity>(DbContext context) where TEntity : class
        {
            // Context settings.
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            // Loading data with <c>Set</c> method.
            return context.Set<TEntity>();
        }

        /// <summary>
        /// Inserts entity data in context.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// Selected entity type.
        /// </typeparam>
        public static void Insert<TEntity>(TEntity entity, DbContext context) where TEntity : class
        {
            // Context settings.
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
        }

        /// <summary>
        /// Inserts entity data in context async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// Selected entity type.
        /// </typeparam>
        public static async void InsertAsync<TEntity>(TEntity entity, DbContext context) where TEntity : class
        {
            // Context settings.
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            context.Entry(entity).State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Inserts several entities data in context.
        /// </summary>
        /// <param name="entities">
        /// The list of entities.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// Selected entity type.
        /// </typeparam>
        public static void Insert<TEntity>(IEnumerable<TEntity> entities, DbContext context) where TEntity : class
        {
            // Context settings.
            // Switch off auto detect changes and validation for optimisation purpuses.
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;

            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            foreach (TEntity entity in entities)
            {
                context.Entry(entity).State = EntityState.Added;
            }

            context.SaveChanges();

            context.Configuration.AutoDetectChangesEnabled = true;
            context.Configuration.ValidateOnSaveEnabled = true;
        }

        /// <summary>
        /// Inserts several entities data in context async.
        /// </summary>
        /// <param name="entities">
        /// The list of entities.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// Selected entity type.
        /// </typeparam>
        public static async void InsertAsync<TEntity>(IEnumerable<TEntity> entities, DbContext context) where TEntity : class
        {
            // Context settings.
            // Switch off auto detect changes and validation for optimisation purpuses.
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;

            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            foreach (TEntity entity in entities)
            {
                context.Entry(entity).State = EntityState.Added;
            }

            await context.SaveChangesAsync();

            context.Configuration.AutoDetectChangesEnabled = true;
            context.Configuration.ValidateOnSaveEnabled = true;
        }

        /// <summary>
        /// Updates entity data in context.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// Selected entity type.
        /// </typeparam>
        public static void Update<TEntity>(TEntity entity, DbContext context) where TEntity : class
        {
            // Context settings.
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        /// <summary>
        /// Updates entity data in context async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// Selected entity type.
        /// </typeparam>
        /// <returns>
        /// True if operation completed successfully.
        /// </returns>
        public static async Task<bool> UpdateAsync<TEntity>(TEntity entity, DbContext context) where TEntity : class
        {
            // Context settings.
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes data in context.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// Selected entity type.
        /// </typeparam>
        public static void Delete<TEntity>(TEntity entity, DbContext context) where TEntity : class
        {
            // Context settings.
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        /// <summary>
        /// Deletes data in context async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// Selected entity type.
        /// </typeparam>
        public static async void DeleteAsync<TEntity>(TEntity entity, DbContext context) where TEntity : class
        {
            // Context settings.
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

            context.Entry(entity).State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
        #endregion
    }
}
