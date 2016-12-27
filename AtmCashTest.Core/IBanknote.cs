using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmCashTest.Core
{
    public interface IBanknote
    {
        int Id { get; set; }

        int Nominal { get; set; }

        int Count { get; set; }
    }
}
