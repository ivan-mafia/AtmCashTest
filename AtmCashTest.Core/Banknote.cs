namespace AtmCashTest.Core
{
    public class Banknote : IBanknote
    {
        public int Id { get; set; }

        public int Nominal { get; set; }

        public int Count { get; set; }
    }
}