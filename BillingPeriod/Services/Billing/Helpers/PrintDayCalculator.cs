namespace BillingPeriod.Services.Helpers
{
    public class PrintDayCalculator
    {
        public DateTime CalculatePrintDay(DateTime finalDateofTheRow, int printDay)
        {
            // Variable para comprobar si ya se ha hecho este printDay
            DateTime lastPrintDay = new DateTime();

            // ANTES DE AGREGAR EL DÍA PARA EL NEWPRINTDAY, SE VALIDA SI ES ACEPTABLE
            int maxDayInMonth = DateTime.DaysInMonth(finalDateofTheRow.Year, finalDateofTheRow.Month);

            int day = Math.Min(maxDayInMonth, printDay);


            DateTime newPrintDate = new DateTime(finalDateofTheRow.Year, finalDateofTheRow.Month, day);


            // Si  la nueva fecha de impresión ya se ha hecho, se toma la finalDateofTheRow
            if (newPrintDate == lastPrintDay)
            {
                return finalDateofTheRow;
            }

            // Se asigna la fecha regresada como lastPrintDay
            lastPrintDay = newPrintDate;

            return newPrintDate;

        }
    }
}
