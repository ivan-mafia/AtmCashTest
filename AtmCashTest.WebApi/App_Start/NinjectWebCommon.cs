// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NinjectWebCommon.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the NinjectWebCommon type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AtmCashTest.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AtmCashTest.WebApi.App_Start.NinjectWebCommon), "Stop")]

// ReSharper disable once CheckNamespace
namespace AtmCashTest.WebApi.App_Start
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web;

    using AtmCashTest.Core;
    using AtmCashTest.Data.DbContext;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using NLog;

    /// <summary>
    /// The <c>ninject</c> web common.
    /// </summary>
    public static class NinjectWebCommon
    {
        /// <summary>
        /// The <c>bootstrapper</c>.
        /// </summary>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                kernel.Bind<IRepositoryService>().To<RepositoryService>();
                kernel.Bind<IAtmOperations>().To<AtmOperations>();
                // ReSharper disable once PossibleNullReferenceException
                kernel.Bind<ILogger>().ToMethod(p => LogManager.GetCurrentClassLogger(p.Request.Target.Member.DeclaringType));

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        // ReSharper disable once UnusedParameter.Local
        private static void RegisterServices(IKernel kernel)
        {
        }
    }
}
