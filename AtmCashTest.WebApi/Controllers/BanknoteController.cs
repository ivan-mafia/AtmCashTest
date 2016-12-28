// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BanknoteController.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the Banknote Controller type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using AtmCashTest.Core;
    using AtmCashTest.Data.DbContext;

    using NLog;

    /// <summary>
    /// The banknote controller.
    /// </summary>
    public class BanknoteController : ApiController
    {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="BanknoteController"/> class.
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
        public BanknoteController(IRepositoryService repositoryService, IAtmOperations atmOperations, ILogger logger)
        {
            this.m_repositoryService = repositoryService;
            this.m_atmOperations = atmOperations;
            this.m_logger = logger;
        }

        /// <summary>
        /// The post cash.
        /// </summary>
        /// <param name="banknotes">
        /// The banknotes.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IEnumerable<IBanknote>> PostCash(Banknote[] banknotes)
        {
            this.m_logger.Log(LogLevel.Info, "Post Cash started.");
            return await this.m_repositoryService.AddBanknotesAsync(banknotes);
        }

        /// <summary>
        /// The get cash.
        /// </summary>
        /// <param name="sum">
        /// The sum.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IEnumerable<IBanknote>> GetCash(int sum)
        {
            this.m_logger.Log(LogLevel.Info, "Get Cash started.");
            var atmBanknotes = await this.m_repositoryService.GetAllBanknotesAsync();
            var cashOutBanknotes = this.m_atmOperations.GetCash(atmBanknotes, sum).Where(banknote => banknote.Count > 0).ToList();
            var isCashedOut = await this.m_repositoryService.CashOutBanknotesAsync(cashOutBanknotes);
            if (isCashedOut)
            {
                return cashOutBanknotes;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// The get balance.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<int> GetBalance()
        {
            this.m_logger.Log(LogLevel.Info, "Get Balance started.");
            var banknotes = await this.m_repositoryService.GetAllBanknotesAsync();
            int sum = banknotes.Aggregate(0, (i, banknote) => i + (banknote.Nominal * banknote.Count));
            this.m_logger.Log(LogLevel.Info, "Get Balance completed. Balance - {0}.", sum);
            return sum;
        }
    }
}
