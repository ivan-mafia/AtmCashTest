using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AtmCashTest.WcfService
{
    using System.ServiceModel.Description;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using AtmCashTest.Core;
    using AtmCashTest.Data.DbContext;
    using AtmCashTest.WcfService.Models;

    using AutoMapper;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AtmCashService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AtmCashService.svc or AtmCashService.svc.cs at the Solution Explorer and start debugging.
    public class AtmCashService : IAtmCashService
    {
        //public static void Configure(ServiceConfiguration config)
        //{
        //    ServiceEndpoint se = new ServiceEndpoint(new ContractDescription("IService1"), new BasicHttpBinding(), new EndpointAddress("basic"));
        //    se.Behaviors.Add(new MyEndpointBehavior());
        //    config.AddServiceEndpoint(se);

        //    config.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
        //    config.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
        //    new AutoMapperConfig();
        //}

        public IEnumerable<BanknoteContract> PutCash(IEnumerable<BanknoteContract> banknotes)
        {
            try
            {
                var repositoryService = new RepositoryService();

                return Mapper.Map<IEnumerable<IBanknote>, List<BanknoteContract>>(repositoryService.AddBanknotes(banknotes));
            }
            catch (InvalidOperationException ex)
            {
                throw new FaultException<ImpossibleCashOutFault>(new ImpossibleCashOutFault
                {
                    Result = false,
                    Message = ex.Message,
                    Description = "Fault!!!"
                });
            }
        }

        public IEnumerable<BanknoteContract> GetCash(int sum)
        {
            try
            {
                var repositoryService = new RepositoryService();
                var atmBanknotes = repositoryService.GetAllBanknotes();
                var Atm = new AtmOperations();
                var cashOutBanknotes = Atm.GetCash(atmBanknotes, sum);
                var isCashedOut = repositoryService.CashOutBanknotes(cashOutBanknotes);
                if (isCashedOut)
                {
                    return Mapper.Map<IEnumerable<IBanknote>, List<BanknoteContract>>(cashOutBanknotes);
                }
                else
                {
                    throw new FaultException<ImpossibleCashOutFault>(new ImpossibleCashOutFault
                    {
                        Result = false,
                        Message = "Server error",
                        Description = "Fault!!!"
                    }, "Server error", new FaultCode("2"), "action");
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new FaultException<ImpossibleCashOutFault>(new ImpossibleCashOutFault
                {
                    Result = false,
                    Message = ex.Message,
                    Description = "Fault!!!"
                }, ex.Message, new FaultCode("2"), "action");
            }
        }

        //public IList<BanknoteContract> GetCashOld(int sum)
        //{
        //    var atm = new AtmOperations();
        //    List<BanknoteContract> atmBanknotes = new List<BanknoteContract>
        //                                 {
        //                                     new BanknoteContract {Id = 1, Nominal = 100, Count = 1},
        //                                     new BanknoteContract {Id = 2, Nominal = 60, Count = 1},
        //                                     new BanknoteContract {Id = 3, Nominal = 40, Count = 3},
        //                                     new BanknoteContract {Id = 4, Nominal = 5000, Count = 0}
        //                                 };
        //    try
        //    {
        //        return atm.GetCash(atmBanknotes, sum).ToList();
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        throw new FaultException<ImpossibleCashOutFault>(new ImpossibleCashOutFault
        //        {
        //            Result = false,
        //            Message = ex.Message,
        //            Description = "Fault!!!"
        //        });
        //    }
        //}

        public int GetAccountSum()
        {
            try
            {
                var repo = new RepositoryService();
                int sum = repo.GetAllBanknotes().Aggregate(0, (i, banknote) => i + banknote.Nominal * banknote.Count);
                return sum;
            }
            catch (Exception ex)
            {
                throw new FaultException<ImpossibleCashOutFault>(new ImpossibleCashOutFault
                {
                    Result = false,
                    Message = ex.Message,
                    Description = "Fault!!!"
                });
            }
        }

        /*public async Task<string> GetMessagesAsync(string msg)
        {
            var tcs = new TaskCompletionSource<string>();
            var task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return "Return from Server : " + msg;
            });
            return await task.ConfigureAwait(false);
        }*/
    }
}
