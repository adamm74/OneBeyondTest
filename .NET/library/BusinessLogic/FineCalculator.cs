using System.Diagnostics.Eventing.Reader;

namespace OneBeyondApi.BusinessLogic
{
    public class FineCalculator
    {
        public double CalculateLateReturnFine(DateTime returnDate, DateTime dueDate)
        {
            var daysLate = (returnDate - dueDate).Days;
            var cost = daysLate * 0.05;

            return Math.Round(cost, 2);
        }
    }
}
