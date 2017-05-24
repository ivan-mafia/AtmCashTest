// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBanknote.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the IBanknote type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Core
{
    /// <summary>
    /// The Banknote interface.
    /// </summary>
    public interface IBanknote
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Gets or sets the nominal.
        /// </summary>
        int Nominal { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        int Count { get; set; }
        #endregion
    }
}
