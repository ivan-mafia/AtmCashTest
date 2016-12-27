using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtmCashTest.WcfService.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ImpossibleCashOutFault
    {
        [DataMember]
        public bool Result { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}