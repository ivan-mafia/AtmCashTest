// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the BanknoteCatel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WpfClient.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.ServiceModel;
    using System.Windows;

    using AtmCashTest.WpfClient.AtmCashService;
    using AtmCashTest.WpfClient.Models;

    using Catel;
    using Catel.Data;
    using Catel.MVVM;
    using Catel.Services;

    using NLog;

    /// <summary>
    /// View model for main window.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Public Fields
        /// <summary>
        /// Register the IsCommandsEnabled property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsCommandsEnabledProperty = RegisterProperty("IsCommandsEnabled", typeof(bool));

        /// <summary>
        /// Register the BalanceSum property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BalanceSumProperty = RegisterProperty("BalanceSum", typeof(int));

        /// <summary>
        /// Register the CashOutSum property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CashOutSumProperty = RegisterProperty("CashOutSum", typeof(int));

        /// <summary>
        /// Register the CashInBanknotes property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CashInBanknotesProperty = RegisterProperty("CashInBanknotes", typeof(ObservableCollection<BanknoteCatel>));
        #endregion

        #region Private Fields
        /// <summary>
        /// The Logger.
        /// </summary>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The message service.
        /// </summary>
        private readonly IMessageService m_messageService;

        /// <summary>
        /// The please wait service.
        /// </summary>
        private readonly IPleaseWaitService m_pleaseWaitService;

        /// <summary>
        /// The m_title.
        /// </summary>
        private readonly string m_title = (string)Application.Current.FindResource("MainTitle");
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        /// <param name="messageService">The message service.</param>
        /// <param name="pleaseWaitService">The please wait service.</param>
        public MainWindowViewModel(IMessageService messageService, IPleaseWaitService pleaseWaitService)
        {
            Argument.IsNotNull(() => messageService);
            Argument.IsNotNull(() => pleaseWaitService);

            this.m_messageService = messageService;
            this.m_pleaseWaitService = pleaseWaitService;
            this.GetBalance = new Command(this.OnGetBalanceExecute);
            this.CashIn = new Command(this.OnCashInExecute);
            CashOut = new Command(OnCashOutExecute);
            this.IsCommandsEnabled = true;
            this.CashInBanknotes = new ObservableCollection<BanknoteCatel>
                                       {
                                           new BanknoteCatel { Id = 1, Nominal = 100, Count = 0 },
                                           new BanknoteCatel { Id = 2, Nominal = 500, Count = 0 },
                                           new BanknoteCatel { Id = 3, Nominal = 1000, Count = 0 },
                                           new BanknoteCatel { Id = 4, Nominal = 5000, Count = 0 },
                                       };
            this.CashOutSum = 100;
            this.OnGetBalanceExecute();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the title of the view model.
        /// </summary>
        /// <value>The title.</value>
        public override string Title => this.m_title;

        /// <summary>
        /// Gets or sets a value indicating whether is commands enabled.
        /// </summary>
        public bool IsCommandsEnabled
        {
            get { return this.GetValue<bool>(IsCommandsEnabledProperty); }
            set { this.SetValue(IsCommandsEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public int BalanceSum
        {
            get { return this.GetValue<int>(BalanceSumProperty); }
            set { this.SetValue(BalanceSumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public int CashOutSum
        {
            get { return this.GetValue<int>(CashOutSumProperty); }
            set { this.SetValue(CashOutSumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ObservableCollection<BanknoteCatel> CashInBanknotes
        {
            get { return this.GetValue<ObservableCollection<BanknoteCatel>>(CashInBanknotesProperty); }
            set { this.SetValue(CashInBanknotesProperty, value); }
        }

        /// <summary>
        /// Gets the GetBalance command.
        /// </summary>
        public Command GetBalance { get; private set; }

        /// <summary>
        /// Gets the CashIn command.
        /// </summary>
        public Command CashIn { get; private set; }

        /// <summary>
        /// Gets the CashOut command.
        /// </summary>
        public Command CashOut { get; private set; }
        #endregion

        #region Private Methods
        /// <summary>
        /// Method to invoke when the GetBalance command is executed.
        /// </summary>
        private async void OnGetBalanceExecute()
        {
            this.IsCommandsEnabled = false;
            logger.Log(LogLevel.Info, "Get Balance started.");
            try
            {
                using (AtmCashServiceClient proxy = new AtmCashServiceClient())
                {
                    this.BalanceSum = await proxy.GetBalanceAsync();
                    logger.Log(LogLevel.Info, "Get Balance completed. Balance - {0}.", this.BalanceSum);
                }
            }
            catch (FaultException<AtmServiceFault> ex)
            {
                logger.Log(LogLevel.Error, ex, "Get Balance returned error.");
                await this.m_messageService.ShowAsync(ex.Detail.Message);
            }
            finally
            {
                this.IsCommandsEnabled = true;
            }
        }

        /// <summary>
        /// Method to invoke when the CashIn command is executed.
        /// </summary>
        private async void OnCashInExecute()
        {
            this.IsCommandsEnabled = false;
            logger.Log(LogLevel.Info, "Cash In started.");
            try
            {
                var banknotes = (from banknote in this.CashInBanknotes
                                 where banknote.Count > 0
                                 select new BanknoteContract { Count = banknote.Count, Nominal = banknote.Nominal }).ToList();
                using (AtmCashServiceClient proxy = new AtmCashServiceClient())
                {
                    var notAcceptedBanknotes = await proxy.PutCashAsync(banknotes.ToArray());
                    this.BalanceSum = await proxy.GetBalanceAsync();
                    if (notAcceptedBanknotes != null && notAcceptedBanknotes.Length > 0)
                    {
                        await m_messageService.ShowAsync("Not accepted");
                        foreach (var banknote in this.CashInBanknotes)
                        {
                            if (notAcceptedBanknotes.FirstOrDefault(b => b.Nominal == banknote.Nominal) == null)
                            {
                                banknote.Count = 0;
                            }
                        }
                    }
                    else
                    {
                        foreach (var banknote in this.CashInBanknotes)
                        {
                            banknote.Count = 0;
                        }
                    }

                    logger.Log(LogLevel.Info, "Cash In completed.");
                }
            }
            catch (FaultException<AtmServiceFault> ex)
            {
                logger.Log(LogLevel.Error, ex, "Cash In returned error.");
                await this.m_messageService.ShowAsync(ex.Detail.Message);
            }
            finally
            {
                this.IsCommandsEnabled = true;
            }

        }

        /// <summary>
        /// Method to invoke when the CashOut command is executed.
        /// </summary>
        private async void OnCashOutExecute()
        {
            logger.Log(LogLevel.Info, "Cash Out started. Requested Sum - {0}", this.CashOutSum);
            List<BanknoteContract> banknotes = new List<BanknoteContract>();
            IsCommandsEnabled = false;
            this.m_pleaseWaitService.Show();
            try
            {
                using (AtmCashServiceClient proxy = new AtmCashServiceClient())
                {
                    var cashOutBanknotes = await proxy.GetCashAsync(this.CashOutSum);
                    banknotes.AddRange(cashOutBanknotes);
                    this.BalanceSum = await proxy.GetBalanceAsync();
                }
                string message = banknotes.Where(banknote => banknote.Count > 0)
                    .Aggregate(
                        "Cashed out: ",
                        (current, banknote) =>
                        current + string.Format("{1} of \"{0}\" banknotes, ", banknote.Nominal, banknote.Count));
                await this.m_messageService.ShowAsync(message.Substring(0, message.Length - 2));
                logger.Log(LogLevel.Info, "Cash Out completed.");
            }
            catch (FaultException<AtmServiceFault> ex)
            {
                logger.Log(LogLevel.Error, "Get Cash couldn't cashout this sum({0}).", this.CashOutSum);
                await this.m_messageService.ShowAsync(ex.Detail.Message);
            }
            finally
            {
                IsCommandsEnabled = true;
                this.m_pleaseWaitService.Hide();
            }
        }
        #endregion
    }
}
