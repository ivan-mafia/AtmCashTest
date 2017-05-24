// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Banknote.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the Banknote type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Core
{
    /// <summary>
    /// The banknote.
    /// </summary>
    public class Banknote : IBanknote
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the nominal.
        /// </summary>
        public int Nominal { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }
        #endregion
    }
}
