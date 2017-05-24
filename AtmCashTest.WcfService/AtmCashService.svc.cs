// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtmCashService.svc.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the AtmCashService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WcfService
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Data.SqlClient;
    using System.Linq;
    using System.ServiceModel;

    using AtmCashTest.Core;
    using AtmCashTest.Data.DbContext;
    using AtmCashTest.WcfService.Models;

    using AutoMapper;

    using NLog;

    /// <summary>
    /// The <c>atm</c> cash service.
    /// </summary>
    public class AtmCashService : IAtmCashService
    {
        #region Private Fields
        /// <summary>
        /// The repository service.
        /// </summary>
        private readonly IRepositoryService m_repositoryService;

        /// <summary>
        /// The <c>atm</c> operations.
        /// </summary>
        private readonly IAtmOperations m_atmOperations;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger m_logger;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AtmCashService"/> class.
        /// </summary>
        /// <param name="repositoryService">
        /// The repository service.
        /// </param>
        /// <param name="atmOperations">
        /// The <c>atm</c> operations.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        public AtmCashService(IRepositoryService repositoryService, IAtmOperations atmOperations, ILogger logger)
        {
            this.m_repositoryService = repositoryService;
            this.m_atmOperations = atmOperations;
            this.m_logger = logger;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// The put cash.
        /// </summary>
        /// <param name="banknotes">
        /// The banknotes.
        /// </param>
        /// <returns>
        /// The list of banknotes.
        /// </returns>
        /// <exception cref="FaultException{T}">
        /// Throws fault exception if exception was thrown.
        /// </exception>
        public IEnumerable<BanknoteContract> PutCash(IEnumerable<BanknoteContract> banknotes)
        {
            try
            {
                this.m_logger.Log(LogLevel.Info, "Put Cash started.");
                IEnumerable<BanknoteContract> notAcceptedBanknotes = Mapper.Map<IEnumerable<IBanknote>, List<BanknoteContract>>(this.m_repositoryService.AddBanknotes(banknotes));
                this.m_logger.Log(LogLevel.Info, "Put Cash completed.");
                return notAcceptedBanknotes;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is DbUpdateConcurrencyException || ex is DbUpdateException || ex is DbEntityValidationException || ex is SqlException)
            {
                this.m_logger.Log(LogLevel.Error, ex, "Put Cash returned error.");
                throw new FaultException<AtmServiceFault>(new AtmServiceFault
                {
                    Result = false,
                    Message = ex.Message,
                    Description = ex.HelpLink
                });
            }
        }

        /// <summary>
        /// The get cash.
        /// </summary>
        /// <param name="sum">
        /// The selected sum.
        /// </param>
        /// <returns>
        /// The list of banknotes.
        /// </returns>
        /// <exception cref="FaultException{T}">
        /// Throws fault exception if exception was thrown.
        /// </exception>
        public IEnumerable<BanknoteContract> GetCash(int sum)
        {
            try
            {
                this.m_logger.Log(LogLevel.Info, "Get Cash started. Requested Sum - {0}", sum);
                if (sum <= 0)
                {
                    throw new InvalidOperationException("Couldn't cashout this sum");
                }

                var atmBanknotes = this.m_repositoryService.GetAllBanknotes();
                var cashOutBanknotes = this.m_atmOperations.GetCash(atmBanknotes, sum).Where(b => b.Count > 0).ToList();
                var isCashedOut = this.m_repositoryService.CashOutBanknotes(cashOutBanknotes);
                if (isCashedOut)
                {
                    this.m_logger.Log(LogLevel.Info, "Get Cash completed.");
                    return Mapper.Map<IEnumerable<IBanknote>, List<BanknoteContract>>(cashOutBanknotes);
                }
                else
                {
                    this.m_logger.Log(LogLevel.Error, "Get Cash couldn't cashout this sum({0}).", sum);
                    throw new FaultException<AtmServiceFault>(new AtmServiceFault
                    {
                        Result = false,
                        Message = "Couldn't cashout this sum",
                        Description = "Couldn't cashout this sum. Please try another sum."
                    });
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is DbUpdateConcurrencyException || ex is DbUpdateException || ex is DbEntityValidationException || ex is SqlException)
            {
                this.m_logger.Log(LogLevel.Error, ex, "Get Cash returned error.");
                throw new FaultException<AtmServiceFault>(new AtmServiceFault
                {
                    Result = false,
                    Message = ex.Message,
                    Description = ex.HelpLink
                });
            }
        }

        /// <summary>
        /// The get balance.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        /// <exception cref="FaultException{T}">
        /// Throws fault exception if exception was thrown.
        /// </exception>
        public int GetBalance()
        {
            try
            {
                this.m_logger.Log(LogLevel.Info, "Get Balance started.");
                int sum = this.m_repositoryService.GetAllBanknotes().Aggregate(0, (i, banknote) => i + (banknote.Nominal * banknote.Count));
                this.m_logger.Log(LogLevel.Info, "Get Balance completed. Balance - {0}.", sum);
                return sum;
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is DbUpdateConcurrencyException || ex is DbUpdateException || ex is DbEntityValidationException || ex is SqlException)
            {
                this.m_logger.Log(LogLevel.Error, ex, "Get Balance returned error.");
                throw new FaultException<AtmServiceFault>(new AtmServiceFault
                {
                    Result = false,
                    Message = ex.Message,
                    Description = ex.HelpLink
                });
            }
        }
        #endregion
    }
}
