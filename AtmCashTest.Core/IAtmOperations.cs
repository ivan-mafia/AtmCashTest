// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAtmOperations.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the IAtmOperations type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// The <c>Atm</c> operations interface.
    /// </summary>
    public interface IAtmOperations
    {
        #region Public Methods
        /// <summary>
        /// The get cash.
        /// </summary>
        /// <param name="atmBanknotes">
        /// The <c>atm</c> banknotes.
        /// </param>
        /// <param name="amount">
        /// The amount.
        /// </param>
        /// <returns>
        /// The The list of banknotes.
        /// </returns>
        IEnumerable<IBanknote> GetCash(IEnumerable<IBanknote> atmBanknotes, int amount);
        #endregion
    }
}
