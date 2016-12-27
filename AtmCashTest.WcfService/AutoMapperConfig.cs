using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtmCashTest.WcfService
{
    using AtmCashTest.Core;
    using AtmCashTest.WcfService.Models;

    using AutoMapper;

    public class AutoMapperConfig
    {
        public AutoMapperConfig()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<IBanknote, BanknoteContract>());
        }
    }
}