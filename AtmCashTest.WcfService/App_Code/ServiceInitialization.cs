// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceInitialization.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the ServiceInitialization type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WcfService.App_Code
{
    /// <summary>
    /// Service initialization.
    /// </summary>
    public class ServiceInitialization
    {
        /// <summary>
        /// App initialize method.
        /// </summary>
        public static void AppInitialize()
        {
            // AutoMapper initialization.
            new AutoMapperConfig();
        }
    }
}