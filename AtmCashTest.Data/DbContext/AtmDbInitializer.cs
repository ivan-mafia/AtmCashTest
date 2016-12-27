using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtmCashTest.Data.DbContext
{
    using System.Collections;
    using System.Data.Entity;

    public class AtmDbInitializer : DropCreateDatabaseIfModelChanges<AtmDbContext>
    {
        protected override void Seed(AtmDbContext context)
        {
            IList<BanknoteEntity> defaulBanknotes = new List<BanknoteEntity>
                                                  {
                                                      new BanknoteEntity() { Nominal = 100, Count = 0 },
                                                      new BanknoteEntity() { Nominal = 500, Count = 0 },
                                                      new BanknoteEntity() { Nominal = 1000, Count = 0 },
                                                      new BanknoteEntity() { Nominal = 5000, Count = 0 }
                                                  };
            context.Banknotes.AddRange(defaulBanknotes);
            base.Seed(context);
        }
    }
}