// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BanknoteEntity.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the BanknoteEntity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Data.DbContext
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using AtmCashTest.Core;

    /// <summary>
    /// The banknote entity.
    /// </summary>
    [Table("banknote")]
    public class BanknoteEntity : IBanknote
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the nominal.
        /// </summary>
        [Index("IX_Banknote_Nominal", 1, IsUnique = true)]
        [Range(0, int.MaxValue, ErrorMessage = "Nominal must be positive")]
        public int Nominal { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Count must be positive")]
        public int Count { get; set; }
        #endregion
    }
}
