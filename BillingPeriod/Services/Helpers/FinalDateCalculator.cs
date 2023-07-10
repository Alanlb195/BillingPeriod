namespace BillingPeriod.Services.Helpers
{
    // Toda fecha final siempre toma en cuenta la initialDateofTheRow y agrega la periodicidad en meses

    public class FinalDateCalculator
    {
        public DateTime CalculateFinalDate(DateTime initialDateofTheRow, int cuttingDay, int periodicity, DateTime finalDate)
        {
            DateTime finalDateofTheRow = new DateTime();

            int year = initialDateofTheRow.Year;
            int month = initialDateofTheRow.Month;
            int maxDayInMonth = DateTime.DaysInMonth(year, month); // Es el maximo día que acepta el mes ej. feb = 28


            if (initialDateofTheRow.Day == 1)
            {

                if (periodicity == 1)
                {
                    int day = Math.Min(cuttingDay, maxDayInMonth);

                    finalDateofTheRow = new DateTime(year, month, day);
                }
                else
                {
                    int day = Math.Min(cuttingDay, maxDayInMonth);

                    finalDateofTheRow = new DateTime(year, month + periodicity, day);
                }

            }

            else
            {

                int day = cuttingDay;

                finalDateofTheRow = new DateTime(year, month + periodicity, day);

            }

            // Asegurarse de que si la fecha final compuesta es mayor a la fecha final, se tome la fecha final
            if (finalDateofTheRow > finalDate)
            {
                return finalDate;
            }
            return finalDateofTheRow;

        }
    }
}
