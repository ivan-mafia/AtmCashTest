// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Interaction logic for App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WpfClient
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    using Catel.ApiCop;
    using Catel.ApiCop.Listeners;
    using Catel.Logging;

    /// <summary>
    /// Interaction logic for <c>App.xaml</c>.
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class App : Application
    {
        /// <summary>
        /// The log.
        /// </summary>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The on startup.
        /// </summary>
        /// <param name="e">
        /// The startup event args.
        /// </param>
        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            LogManager.AddDebugListener();
#endif

            Log.Info("Starting application");
            Log.Info("Calling base.OnStartup");

            base.OnStartup(e);
        }

        /// <summary>
        /// The on exit.
        /// </summary>
        /// <param name="e">
        /// The exit events args.
        /// </param>
        protected override void OnExit(ExitEventArgs e)
        {
            // Get advisory report in console
            ApiCopManager.AddListener(new ConsoleApiCopListener());
            ApiCopManager.WriteResults();

            base.OnExit(e);
        }
    }
}
