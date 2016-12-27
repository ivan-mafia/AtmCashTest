using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtmCashTest.Data.DbContext
{
    using AtmCashTest.Core;

    public class RepositoryService
    {
        public IEnumerable<IBanknote> GetAllBanknotes()
        {
            using (AtmDbContext context = new AtmDbContext())
            {
                return Repository.Select<BanknoteEntity>(context).ToList();
            }
        }

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