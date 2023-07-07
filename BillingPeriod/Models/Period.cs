namespace BillingPeriod.Models
{
    public class Period
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public int Periodicity { get; set; }
        public int CuttingDay { get; set; }
        public int PrintDay { get; set; }
    }

    public class PeriodRow
    {
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public DateTime PrintDay { get; set; }
    }
}
