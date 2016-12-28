// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AutoMapperConfig.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the AutoMapperConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WcfService
{
    using AtmCashTest.Core;
    using AtmCashTest.WcfService.Models;

    using AutoMapper;

    /// <summary>
    /// The auto mapper config.
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperConfig"/> class.
        /// </summary>
        public AutoMapperConfig()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<IBanknote, BanknoteContract>());
        }
    }
}
