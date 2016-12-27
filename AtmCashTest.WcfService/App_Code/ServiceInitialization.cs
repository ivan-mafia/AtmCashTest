using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtmCashTest.WcfService.App_Code
{
    public class ServiceInitialization
    {
        public static void AppInitialize()
        {
            new AutoMapperConfig();
        }
    }
}