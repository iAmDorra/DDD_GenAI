using System.Collections.Generic;
using System.Linq;
// ReSharper disable PossibleMultipleEnumeration

namespace ContiguousPeriod.Tests
{
    public class Periods
    {
        private readonly IEnumerable<Period> periods;

        public Periods(IEnumerable<Period> periods)
        {
            this.periods = periods;
        }
        public IEnumerable<Period> CalculateContiguousPeriods()
        {
            var contiguousPeriods = new List<Period>();
            foreach (var period in periods.Where(p => p.Value == 0))
            {
                UpdateEndDate(contiguousPeriods, period);
            }

            contiguousPeriods.AddRange(periods.Where(p => p.Value != 0));
            return contiguousPeriods.OrderBy(p => p.Start);
        }

        private static void UpdateEndDate(List<Period> contiguousPeriods, Period period)
        {
            var savedZeroPeriod = contiguousPeriods.LastOrDefault(p => p.Value == 0);
            if (savedZeroPeriod != null && period.Start == savedZeroPeriod.End.AddDays(1))
            {
                savedZeroPeriod.UpdateEndDate(period.End);
            }
            else
            {
                contiguousPeriods.Add(new Period(period.Start, period.End, 0));
            }
        }
    }
}