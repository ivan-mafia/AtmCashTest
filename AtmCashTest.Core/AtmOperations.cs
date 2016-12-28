// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AtmOperations.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the AtmOperations type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The <c>Atm</c> operations.
    /// </summary>
    public class AtmOperations : IAtmOperations
    {
        /// <summary>
        /// The get cash.
        /// </summary>
        /// <param name="atmBanknotes">
        /// The <c>atm</c> banknotes.
        /// </param>
        /// <param name="amount">
        /// The amount that needed to cash out.
        /// </param>
        /// <returns>
        /// The list of banknotes.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Throws exception when impossible to cash out selected sum.
        /// </exception>
        public IEnumerable<IBanknote> GetCash(IEnumerable<IBanknote> atmBanknotes, int amount)
        {
            IList<IBanknote> cashOut = new List<IBanknote>();
            var banknotes = atmBanknotes as IList<IBanknote> ?? atmBanknotes.ToList();
            List<IBanknote> atmB = new List<IBanknote>(banknotes);

            while (true)
            {
                int change;

                IBanknote currentMax;

                // Get biggest unused nominal
                if (atmB.Count > cashOut.Count)
                {
                    currentMax = atmB.Where(x => cashOut.All(p => p.Nominal != x.Nominal)).Aggregate((agg, next) => next.Nominal > agg.Nominal ? next : agg);
                }
                else
                {
                    throw new InvalidOperationException("Couldn't cashout this sum");
                }

                // Needed amount of banknotes of this nominal
                int neededCount = amount / currentMax.Nominal;

                // If needed banknotes count is more then atm has, then get all this banknotes from atm.
                if (neededCount > currentMax.Count)
                {
                    change = amount - (currentMax.Nominal * currentMax.Count);
                    neededCount = currentMax.Count;
                }
                else
                {
                    change = amount - (currentMax.Nominal * neededCount);
                }

                // All sum is out.
                if (change == 0)
                {
                    cashOut.Add(new Banknote { Id = currentMax.Id, Nominal = currentMax.Nominal, Count = neededCount });
                    return cashOut;
                }

                // Left sum if less then minimum banknote nominal
                if (change < banknotes.Aggregate((agg, next) => next.Nominal < agg.Nominal ? next : agg).Nominal)
                {
                    // Needed count decrease by 1, to try to find sum with more tiny banknotes
                    neededCount -= 1;
                    cashOut.Add(new Banknote { Id = currentMax.Id, Nominal = currentMax.Nominal, Count = neededCount });
                    amount = amount - (currentMax.Nominal * neededCount);
                    continue;
                }

                atmB.Find(x => x.Nominal == currentMax.Nominal).Count -= neededCount;
                cashOut.Add(new Banknote { Id = currentMax.Id, Nominal = currentMax.Nominal, Count = neededCount });

                amount = change;
            }
        }
    }
}
