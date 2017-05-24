// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtmServiceFault.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the AtmServiceFault type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WcfService.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The <c>atm</c> service fault.
    /// </summary>
    [DataContract]
    public class AtmServiceFault
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether result.
        /// </summary>
        [DataMember]
        public bool Result { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        #endregion
    }
}
