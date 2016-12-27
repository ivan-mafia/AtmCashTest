using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AtmCashTest.WcfService
{
    using System.Threading.Tasks;

    using AtmCashTest.WcfService.Models;

    [ServiceContract]
    public interface IAtmCashService
    {
        [OperationContract]
        IEnumerable<BanknoteContract> PutCash(IEnumerable<BanknoteContract> banknotes);

        [OperationContract]
        [FaultContract(typeof(ImpossibleCashOutFault))]
        IEnumerable<BanknoteContract> GetCash(int sum);

        [OperationContract]
        int GetAccountSum();
    }
}
