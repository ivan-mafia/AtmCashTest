using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmCashTest.Data.DbContext
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using AtmCashTest.Core;

    [Table("banknote")]
    public class BanknoteEntity : IBanknote
    {
        public int Id { get; set; }

        [Index("IX_Banknote_Nominal", 1, IsUnique = true)]
        [Range(0, int.MaxValue, ErrorMessage = "Nominal must be positive")]
        public int Nominal { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Count must be positive")]
        public int Count { get; set; }
    }
}
