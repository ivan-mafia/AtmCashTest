using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtmCashTest.Core
{
    public class AtmOperations
    {
        public IEnumerable<IBanknote> GetCash(IEnumerable<IBanknote> atmBanknotes, int amount)
        {
            IList<IBanknote> cashOut = new List<IBanknote>();

            List<IBanknote> atmB = new List<IBanknote>(atmBanknotes);


            while (true)
            {
                int change;

                IBanknote currentMax;

                //Get biggest unused nominal
                if (atmB.Count > cashOut.Count)
                {
                    currentMax = atmB.Where(x => cashOut.All(p => p.Nominal != x.Nominal)).Aggregate((agg, next) => next.Nominal > agg.Nominal ? next : agg);
                }
                else
                {
                    throw new InvalidOperationException("Couldn't cashout this sum");
                }

                //Needed amount of banknotes of this nominal
                int neededCount = amount / currentMax.Nominal;

                //If needed banknotes count is more then atm has, then get all this banknotes from atm.
                if (neededCount > currentMax.Count)
                {
                    change = amount - currentMax.Nominal * currentMax.Count;
                    neededCount = currentMax.Count;
                }
                else
                {
                    change = amount - currentMax.Nominal * neededCount;
                }

                // All sum is out.
                if (change == 0)
                {
                    cashOut.Add(new Banknote { Id = currentMax.Id, Nominal = currentMax.Nominal, Count = neededCount });
                    return cashOut;
                }

                // Left sum if less then minimum banknote nominal
                if (change < atmBanknotes.Aggregate((agg, next) => next.Nominal < agg.Nominal ? next : agg).Nominal)
                {
                    // needed count decrease by 1, to try to find sum with more tiny banknotes
                    neededCount -= 1;
                    cashOut.Add(new Banknote { Id = currentMax.Id, Nominal = currentMax.Nominal, Count = neededCount });
                    amount = amount - currentMax.Nominal * neededCount;
                    continue;
                }

                atmB.Find(x => x.Nominal == currentMax.Nominal).Count -= neededCount;
                cashOut.Add(new Banknote { Id = currentMax.Id, Nominal = currentMax.Nominal, Count = neededCount});

                amount = change;
            }
        }
    }
}