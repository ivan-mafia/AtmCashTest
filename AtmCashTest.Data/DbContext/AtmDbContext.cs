﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtmDbContext.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the AtmDbContext type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Data.DbContext
{
    using System.Data.Entity;

    /// <summary>
    /// The <c>atm</c> database context.
    /// </summary>
    public class AtmDbContext : DbContext
    {
        #region Consturctors
        /// <summary>
        /// Initializes a new instance of the <see cref="AtmDbContext"/> class.
        /// </summary>
        public AtmDbContext()
            : base("atmdb")
        {
            // ReSharper disable once RedundantTypeArgumentsOfMethod
            Database.SetInitializer<AtmDbContext>(new AtmDbInitializer());
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the banknotes.
        /// </summary>
        public DbSet<BanknoteEntity> Banknotes { get; set; }
        #endregion
    }
}
