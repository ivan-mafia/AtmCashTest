using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtmCashTest.Data.DbContext
{
    using System.Data.Entity;

    public class AtmDbContext : DbContext
    {
        public AtmDbContext()
            : base("atmdb")
        {
            Database.SetInitializer<AtmDbContext>(new AtmDbInitializer());
        }

        public DbSet<BanknoteEntity> Banknotes { get; set; }
    }
}