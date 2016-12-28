// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAtmCashService.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the IAtmCashService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WcfService
{
    using System.Collections.Generic;
    using System.ServiceModel;

    using AtmCashTest.WcfService.Models;

    /// <summary>
    /// The <c>atm</c> cash service interface.
    /// </summary>
    [ServiceContract]
    public interface IAtmCashService
    {
        /// <summary>
        /// The put cash.
        /// </summary>
        /// <param name="banknotes">
        /// The banknotes.
        /// </param>
        /// <returns>
        /// The list of banknotes.
        /// </returns>
        [OperationContract]
        [FaultContract(typeof(AtmServiceFault))]
        IEnumerable<BanknoteContract> PutCash(IEnumerable<BanknoteContract> banknotes);

        /// <summary>
        /// The get cash.
        /// </summary>
        /// <param name="sum">
        /// The selected sum.
        /// </param>
        /// <returns>
        /// The list of banknotes.
        /// </returns>
        [OperationContract]
        [FaultContract(typeof(AtmServiceFault))]
        IEnumerable<BanknoteContract> GetCash(int sum);

        /// <summary>
        /// The get balance.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [OperationContract]
        [FaultContract(typeof(AtmServiceFault))]
        int GetBalance();
    }
}
