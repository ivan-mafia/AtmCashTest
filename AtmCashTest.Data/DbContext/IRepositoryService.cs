// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryService.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the IRepositoryService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Data.DbContext
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AtmCashTest.Core;

    /// <summary>
    /// The RepositoryService interface.
    /// </summary>
    public interface IRepositoryService
    {
        /// <summary>
        /// The get all banknotes types.
        /// </summary>
        /// <returns>
        /// The list of banknotes types.
        /// </returns>
        IEnumerable<IBanknote> GetAllBanknotes();

        /// <summary>
        /// The get all banknotes types async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>. The list of banknotes types.
        /// </returns>
        Task<IEnumerable<IBanknote>> GetAllBanknotesAsync();

        /// <summary>
        /// The add banknotes to <c>atm</c>.
        /// </summary>
        /// <param name="banknotes">
        /// The added banknotes.
        /// </param>
        /// <returns>
        /// The list of not accepted banknotes.
        /// </returns>
        IEnumerable<IBanknote> AddBanknotes(IEnumerable<IBanknote> banknotes);

        /// <summary>
        /// The add banknotes to <c>atm</c> async.
        /// </summary>
        /// <param name="banknotes">
        /// The added banknotes.
        /// </param>
        /// <returns>
        /// The list of not accepted banknotes.
        /// </returns>
        Task<IEnumerable<IBanknote>> AddBanknotesAsync(IEnumerable<IBanknote> banknotes);

        /// <summary>
        /// The cash out banknotes.
        /// </summary>
        /// <param name="banknotes">
        /// The selected banknotes.
        /// </param>
        /// <returns>
        /// True if operation completed successfully.
        /// </returns>
        bool CashOutBanknotes(IEnumerable<IBanknote> banknotes);

        /// <summary>
        /// The cash out banknotes async.
        /// </summary>
        /// <param name="banknotes">
        /// The selected banknotes.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>. True if operation completed successfully.
        /// </returns>
        Task<bool> CashOutBanknotesAsync(IEnumerable<IBanknote> banknotes);
    }
}
