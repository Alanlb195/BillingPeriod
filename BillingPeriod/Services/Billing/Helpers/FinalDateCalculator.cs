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
            int initialDay = initialDateofTheRow.Day;
            int maxDayInMonth = DateTime.DaysInMonth(year, month); // Es el maximo día que acepta el mes ej. feb = 28


            if (initialDateofTheRow.Day == 1)
            {

                if (periodicity == 1)
                {
                    int day = Math.Min(cuttingDay, maxDayInMonth);

                    finalDateofTheRow = new DateTime(year, month, day);
                }

            }

            // SI EL DÍA NO EMPIEZA EN 1 Y SI LA PERIODICIDAD NO ES UNO, SIMPLEMENTE SE SUMA LA PERIODICIDAD
            // PEROO SI SE SALTA DE AÑO ESTO CAMBIA! :( 
            if ((month + periodicity) > 12)
            {
                int day = Math.Min(cuttingDay, maxDayInMonth);
                finalDateofTheRow = new DateTime(year, 12, day);
            }
            else
            {
                // SIMPLEMENTE SE SUMA LA PERIODICIDAD YA QUE EL MES NO EXCEDE EL 12 AUN, ES UNA FECHA REPRESENTABLE,
                // PERO COMPROBAR SI ES UN DÍA REPRESENTABLE!!
                int day = Math.Min(initialDay, maxDayInMonth);
                finalDateofTheRow = new DateTime(year, month + periodicity, initialDay);
            }

            // Asegurarse de que si la fecha final compuesta es mayor a la fecha final, se tome la fecha final
            if (finalDateofTheRow > finalDate)
            {
                finalDateofTheRow = finalDate;
            }
            return finalDateofTheRow;


        }
    }
}
