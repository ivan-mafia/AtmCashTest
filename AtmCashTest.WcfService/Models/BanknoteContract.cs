using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtmCashTest.WcfService.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    using AtmCashTest.Core;

    [DataContract]
    public class BanknoteContract : IBanknote
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [Index("IX_Banknote_Nominal", 1, IsUnique = true)]
        [Range(0, int.MaxValue, ErrorMessage = "Nominal must be positive")]
        public int Nominal { get; set; }

        [DataMember]
        [Range(0, int.MaxValue, ErrorMessage = "Count must be positive")]
        public int Count { get; set; }
    }
}