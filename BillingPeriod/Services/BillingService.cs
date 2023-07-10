using BillingPeriod.Models;
using BillingPeriod.Services.Helpers;

namespace BillingPeriod.Services
{
    public class BillingService : IBillingService
    {
        private readonly FinalDateCalculator _finalDateCalculator;
        private readonly PrintDayCalculator _printDayCalculator;

        public BillingService(
            FinalDateCalculator finalDateCalculator,
            PrintDayCalculator printDayCalculator)
        {
            _finalDateCalculator = finalDateCalculator;
            _printDayCalculator = printDayCalculator;
        }

        public List<PeriodRow> GeneratePeriodRows(Period period)
        {
            // Valores en general a manipular para cada periodo
            List<PeriodRow> rows = new List<PeriodRow>();
            DateTime initialDateOfTheRow = period.InitialDate;
            DateTime finalDateofTheRow = period.InitialDate;
            int periodicity = period.Periodicity;
            int cuttingDay = period.CuttingDay;
            int printDay = period.PrintDay;


            while (finalDateofTheRow < period.FinalDate)
            {
                PeriodRow periodRow = new PeriodRow();

                // INITIAL DATE
                periodRow.InitialDate = initialDateOfTheRow;


                // FINAL DATE
                periodRow.FinalDate = _finalDateCalculator.CalculateFinalDate(initialDateOfTheRow, cuttingDay, periodicity, period.FinalDate);
                finalDateofTheRow = periodRow.FinalDate;


                // PRINT DAY
                periodRow.PrintDay = _printDayCalculator.CalculatePrintDay(finalDateofTheRow, printDay);

                // INITIAL DATE
                initialDateOfTheRow = finalDateofTheRow.AddDays(1);

                rows.Add(periodRow);
            }
            return rows;
        }
    }
}
