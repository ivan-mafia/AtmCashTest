// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BanknoteContract.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the BanknoteContract type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WcfService.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    using AtmCashTest.Core;

    /// <summary>
    /// The banknote contract.
    /// </summary>
    [DataContract]
    public class BanknoteContract : IBanknote
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the nominal.
        /// </summary>
        [DataMember]
        [Index("IX_Banknote_Nominal", 1, IsUnique = true)]
        [Range(0, int.MaxValue, ErrorMessage = "Nominal must be positive")]
        public int Nominal { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        [DataMember]
        [Range(0, int.MaxValue, ErrorMessage = "Count must be positive")]
        public int Count { get; set; }
        #endregion
    }
}
