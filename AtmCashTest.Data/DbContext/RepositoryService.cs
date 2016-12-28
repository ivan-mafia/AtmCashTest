// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryService.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the RepositoryService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Data.DbContext
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using AtmCashTest.Core;

    /// <summary>
    /// The repository service.
    /// </summary>
    public class RepositoryService : IRepositoryService
    {
        /// <summary>
        /// The get all banknotes types.
        /// </summary>
        /// <returns>
        /// The list of banknotes types.
        /// </returns>
        public IEnumerable<IBanknote> GetAllBanknotes()
        {
            using (AtmDbContext context = new AtmDbContext())
            {
                return Repository.Select<BanknoteEntity>(context).ToList();
            }
        }

        /// <summary>
        /// The get all banknotes types async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>. The list of banknotes types.
        /// </returns>
        public async Task<IEnumerable<IBanknote>> GetAllBanknotesAsync()
        {
            using (AtmDbContext context = new AtmDbContext())
            {
                return await Repository.Select<BanknoteEntity>(context).ToListAsync();
            }
        }

        /// <summary>
        /// The add banknotes to <c>atm</c>.
        /// </summary>
        /// <param name="banknotes">
        /// The added banknotes.
        /// </param>
        /// <returns>
        /// The list of not accepted banknotes.
        /// </returns>
        public IEnumerable<IBanknote> AddBanknotes(IEnumerable<IBanknote> banknotes)
        {
            IList<IBanknote> notAcceptedBanknotes = null;

            using (AtmDbContext context = new AtmDbContext())
            {
                foreach (var banknote in banknotes)
                {
                    var banknoteDb = Repository.Select<BanknoteEntity>(context).FirstOrDefault(c => c.Nominal == banknote.Nominal);

                    if (banknoteDb != null)
                    {
                        banknoteDb.Count += banknote.Count;
                        Repository.Update(banknoteDb, context);
                    }
                    else
                    {
                        if (notAcceptedBanknotes == null)
                        {
                            notAcceptedBanknotes = new List<IBanknote>();
                        }

                        notAcceptedBanknotes.Add(banknote);
                    }
                }
            }

            return notAcceptedBanknotes;
        }

        /// <summary>
        /// The add banknotes to <c>atm</c> async.
        /// </summary>
        /// <param name="banknotes">
        /// The added banknotes.
        /// </param>
        /// <returns>
        /// The list of not accepted banknotes.
        /// </returns>
        public async Task<IEnumerable<IBanknote>> AddBanknotesAsync(IEnumerable<IBanknote> banknotes)
        {
            IList<IBanknote> notAcceptedBanknotes = null;

            using (AtmDbContext context = new AtmDbContext())
            {
                foreach (var banknote in banknotes)
                {
                    var banknoteDb = await Repository.Select<BanknoteEntity>(context).FirstOrDefaultAsync(c => c.Nominal == banknote.Nominal);

                    if (banknoteDb != null)
                    {
                        banknoteDb.Count += banknote.Count;
                        await Repository.UpdateAsync(banknoteDb, context);
                    }
                    else
                    {
                        if (notAcceptedBanknotes == null)
                        {
                            notAcceptedBanknotes = new List<IBanknote>();
                        }

                        notAcceptedBanknotes.Add(banknote);
                    }
                }
            }

            return notAcceptedBanknotes;
        }

        /// <summary>
        /// The cash out banknotes.
        /// </summary>
        /// <param name="banknotes">
        /// The selected banknotes.
        /// </param>
        /// <returns>
        /// True if operation completed successfully.
        /// </returns>
        public bool CashOutBanknotes(IEnumerable<IBanknote> banknotes)
        {
            using (AtmDbContext context = new AtmDbContext())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    foreach (var banknote in banknotes)
                    {
                        var banknoteDb =
                            Repository.Select<BanknoteEntity>(context).FirstOrDefault(c => c.Nominal == banknote.Nominal);

                        if (banknoteDb != null && banknoteDb.Count - banknote.Count >= 0)
                        {
                            banknoteDb.Count -= banknote.Count;
                            Repository.Update(banknoteDb, context);
                        }
                        else
                        {
                            // Rollback transaction if there is not enough banknotes in atm.
                            dbTransaction.Rollback();
                            return false;
                        }
                    }

                    dbTransaction.Commit();
                }
            }

            return true;
        }

        /// <summary>
        /// The cash out banknotes async.
        /// </summary>
        /// <param name="banknotes">
        /// The selected banknotes.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>. True if operation completed successfully.
        /// </returns>
        public async Task<bool> CashOutBanknotesAsync(IEnumerable<IBanknote> banknotes)
        {
            using (AtmDbContext context = new AtmDbContext())
            {
                using (var dbTransaction = context.Database.BeginTransaction())
                {
                    foreach (var banknote in banknotes)
                    {
                        var banknoteDb =
                            await Repository.Select<BanknoteEntity>(context).FirstOrDefaultAsync(c => c.Nominal == banknote.Nominal);

                        if (banknoteDb != null && banknoteDb.Count - banknote.Count >= 0)
                        {
                            banknoteDb.Count -= banknote.Count;
                            await Repository.UpdateAsync(banknoteDb, context);
                        }
                        else
                        {
                            // Rollback transaction if there is not enough banknotes in atm.
                            dbTransaction.Rollback();
                            return false;
                        }
                    }

                    dbTransaction.Commit();
                }
            }

            return true;
        }
    }
}
