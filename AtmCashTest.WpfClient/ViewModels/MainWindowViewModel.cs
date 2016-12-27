namespace AtmCashTest.WpfClient.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel;

    using Catel.MVVM;
    using System.Threading.Tasks;
    using System.Windows;

    using AtmCashTest.WpfClient.AtmCashService;
    using AtmCashTest.WpfClient.Models;

    using Catel;
    using Catel.Data;
    using Catel.Services;

    public class MainWindowViewModel : ViewModelBase
    {
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

        public MainWindowViewModel(IMessageService messageService, IPleaseWaitService pleaseWaitService)
        {
            Argument.IsNotNull(() => messageService);
            Argument.IsNotNull(() => pleaseWaitService);

            this.m_messageService = messageService;
            this.m_pleaseWaitService = pleaseWaitService;
            this.AccountInfo = new Command(OnAccountInfoExecute);
            this.CashIn = new Command(this.OnCashInExecute);
            CashOut = new Command(OnCashOutExecute);
            this.IsCommandsEnabled = true;
            this.CashInBanknotes = new ObservableCollection<BanknoteCatel>
                                       {
                                           new BanknoteCatel {Id = 1, Nominal = 100, Count = 0},
                                           new BanknoteCatel {Id = 2, Nominal = 500, Count = 0},
                                           new BanknoteCatel {Id = 3, Nominal = 1000, Count = 0},
                                           new BanknoteCatel {Id = 4, Nominal = 5000, Count = 0},
                                       };
            OnAccountInfoExecute();
        }

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        public bool IsCommandsEnabled
        {
            get { return GetValue<bool>(IsCommandsEnabledProperty); }
            set { SetValue(IsCommandsEnabledProperty, value); }
        }

        /// <summary>
        /// Register the IsCommandsEnabled property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IsCommandsEnabledProperty = RegisterProperty("IsCommandsEnabled", typeof(bool), null);

        /// <summary>
            /// Gets or sets the property value.
            /// </summary>
        public int BalanceSum
        {
            get { return GetValue<int>(BalanceSumProperty); }
            set { SetValue(BalanceSumProperty, value); }
        }

        /// <summary>
        /// Register the BalanceSum property so it is known in the class.
        /// </summary>
        public static readonly PropertyData BalanceSumProperty = RegisterProperty("BalanceSum", typeof(int), null);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public int CashOutSum
        {
            get { return GetValue<int>(CashOutSumProperty); }
            set { SetValue(CashOutSumProperty, value); }
        }

        /// <summary>
        /// Register the CashOutSum property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CashOutSumProperty = RegisterProperty("CashOutSum", typeof(int), null);

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public ObservableCollection<BanknoteCatel> CashInBanknotes
        {
            get { return GetValue<ObservableCollection<BanknoteCatel>>(CashInBanknotesProperty); }
            set { SetValue(CashInBanknotesProperty, value); }
        }

        /// <summary>
        /// Register the CashInBanknotes property so it is known in the class.
        /// </summary>
        public static readonly PropertyData CashInBanknotesProperty = RegisterProperty("CashInBanknotes", typeof(ObservableCollection<BanknoteCatel>), null);

        /// <summary>
            /// Gets the AccountInfo command.
            /// </summary>
        public Command AccountInfo { get; private set; }

        /// <summary>
        /// Method to invoke when the AccountInfo command is executed.
        /// </summary>
        private async void OnAccountInfoExecute()
        {
            this.IsCommandsEnabled = false;
            try
            {
                using (AtmCashServiceClient proxy = new AtmCashServiceClient())
                {
                    this.BalanceSum = await proxy.GetAccountSumAsync();
                }
            }
            finally
            {
                this.IsCommandsEnabled = true;
            }
        }

        /// <summary>
            /// Gets the CashIn command.
            /// </summary>
        public Command CashIn { get; private set; }

        /// <summary>
        /// Method to invoke when the CashIn command is executed.
        /// </summary>
        private async void OnCashInExecute()
        {
            this.IsCommandsEnabled = false;
            try
            {
                var banknotes = (from banknote in this.CashInBanknotes
                                 where banknote.Count > 0
                                 select new BanknoteContract { Count = banknote.Count, Nominal = banknote.Nominal }).ToList();
                using (AtmCashServiceClient proxy = new AtmCashServiceClient())
                {
                    var notAcceptedBanknotes = await proxy.PutCashAsync(banknotes.ToArray());
                    this.BalanceSum = await proxy.GetAccountSumAsync();
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
                }
            }
            finally
            {
                this.IsCommandsEnabled = true;
            }

        }

        /// <summary>
            /// Gets the CashOut command.
            /// </summary>
        public Command CashOut { get; private set; }

        /// <summary>
        /// Method to invoke when the CashOut command is executed.
        /// </summary>
        private async void OnCashOutExecute()
        {
            List<BanknoteContract> banknotes = new List<BanknoteContract>();
            IsCommandsEnabled = false;
            this.m_pleaseWaitService.Show();
            try
            {
                using (AtmCashServiceClient proxy = new AtmCashServiceClient())
                {
                    var cashOutBanknotes = await proxy.GetCashAsync(this.CashOutSum);
                    banknotes.AddRange(cashOutBanknotes);
                    this.BalanceSum = await proxy.GetAccountSumAsync();
                }
                string message = banknotes.Where(banknote => banknote.Count > 0).Aggregate("Cashed out: ", (current, banknote) => current + string.Format("{1} of \"{0}\" banknotes, ", banknote.Nominal, banknote.Count));
                await this.m_messageService.ShowAsync(message.Substring(0, message.Length-2));
            }
            catch (FaultException<ImpossibleCashOutFault> ex)
            {
                await this.m_messageService.ShowAsync(ex.Message);
            }
            finally
            {
                IsCommandsEnabled = true;
                this.m_pleaseWaitService.Hide();
            }

        }

        /// <summary>
        /// Gets the title of the view model.
        /// </summary>
        /// <value>The title.</value>
        public override string Title => this.m_title;

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // TODO: subscribe to events here
        }

        protected override async Task CloseAsync()
        {
            // TODO: unsubscribe from events here

            await base.CloseAsync();
        }
    }
}
