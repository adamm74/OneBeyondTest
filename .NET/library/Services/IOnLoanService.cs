using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IOnLoanService
    {
        public List<OnLoanBook> GetBorrowersWithActiveLoans();

    }
}
