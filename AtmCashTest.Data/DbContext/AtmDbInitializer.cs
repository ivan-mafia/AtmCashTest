// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtmDbInitializer.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the AtmDbInitializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Data.DbContext
{
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// The <c>atm</c> database initializer.
    /// </summary>
    public class AtmDbInitializer : DropCreateDatabaseIfModelChanges<AtmDbContext>
    {
        #region Private, Protected Methods
        /// <summary>
        /// The seed. Need to initialize data.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
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
        #endregion
    }
}
