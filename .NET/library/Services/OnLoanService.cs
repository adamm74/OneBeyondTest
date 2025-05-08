using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Services
{
    public class OnLoanService : IOnLoanService
    {
        private readonly IBookStockRepository _bookStockRepository;

        public OnLoanService(IBookStockRepository bookStockRepository)
        {
            _bookStockRepository = bookStockRepository;
        }

        public List<OnLoanBook> GetBorrowersWithActiveLoans()
        {

            var booksOnLoan = (from bookStocks in _bookStockRepository.GetBookStocks()
                            where
                                bookStocks.OnLoanTo != null &&
                                bookStocks.LoanEndDate > DateTime.Now
                            select new OnLoanBook
                            {
                                BorrowerName = bookStocks.OnLoanTo.Name,
                                EmailAddress = bookStocks.OnLoanTo.EmailAddress,
                                BookName = bookStocks.Book.Name
                            }).ToList();

            return booksOnLoan;
        } 
    }
}
