using BillingPeriod.Models;

namespace BillingPeriod.Helpers
{
    public abstract class PeriodRowCalculator
    {

        protected abstract DateTime CalculatePrintDate(DateTime endDate, int printDay);

        public List<PeriodRow> GeneratePeriodRows(Period period)
        {
            List<PeriodRow> periodRows = new List<PeriodRow>();

            // Asignar datos del primer período en la primera fila nunca cambian
            DateTime startDate = period.InitialDate;

            // CASE 1
            // 01/01/2014
            // 25/01/2014
            DateTime endDate = period.InitialDate.AddMonths(period.Periodicity).AddDays(-1);

            // CASE 2
            //31/01/2014
            //25/03/2014
            DateTime endDate2 = period.InitialDate.AddMonths(period.Periodicity);


            if( endDate > endDate2)
            {

            }



            // Calcular la fecha de impresión utilizando el mes y año de endDate y el día de PrintDay
            DateTime printDate = CalculatePrintDate(endDate, period.PrintDay);

            // Crear el primer PeriodRow y agregarlo a la lista
            PeriodRow firstPeriodRow = new PeriodRow
            {
                InitialDate = startDate,
                FinalDate = endDate,
                PrintDay = printDate
            };

            periodRows.Add(firstPeriodRow);

            return periodRows;
        }


        public class DefaultPeriodRowCalculator : PeriodRowCalculator
        {
            protected override DateTime CalculatePrintDate(DateTime endDate, int printDay)
            {
                return new DateTime(endDate.Year, endDate.Month, printDay);
            }
        }


    }
}
