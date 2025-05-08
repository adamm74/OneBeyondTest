using OneBeyondApi.BusinessLogic;
using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Services
{
    public class OnLoanService : IOnLoanService
    {
        private readonly IBookStockRepository _bookStockRepository;
        private readonly IFineRepository _fineRepository;

        public OnLoanService(IBookStockRepository bookStockRepository, IFineRepository fineRepository)
        {
            _bookStockRepository = bookStockRepository;
            _fineRepository = fineRepository;
        }

        public List<OnLoanBook> GetBorrowersWithActiveLoans()
        {
            var booksOnLoan = (from bookStocks in _bookStockRepository.GetBookStocks()
                            where
                                bookStocks.OnLoanTo != null
                            select new OnLoanBook
                            {
                                BorrowerName = bookStocks.OnLoanTo?.Name,
                                EmailAddress = bookStocks.OnLoanTo?.EmailAddress,
                                BookName = bookStocks.Book.Name
                            }).ToList();

            return booksOnLoan;
        }

        public string ReturnBook(string isbn)
        {
            var bookStock = _bookStockRepository.GetBookStocks()
                .FirstOrDefault(x => x.Book != null && x.Book.ISBN == isbn);

            if(bookStock == null)
            {
                return $"{isbn} could not be found.";
            }

            var fine = CalculateReturnFine(bookStock.LoanEndDate);

            if (bookStock.OnLoanTo != null) {
                AddBorrowerFine(bookStock.OnLoanTo, fine);
            }

            bookStock.OnLoanTo = null;
            bookStock.LoanEndDate = null;

            _bookStockRepository.ReturnBook(bookStock);

            return $"{bookStock.Book.Name} was returned. There is a £{fine} fine to pay.";
        }

        private double CalculateReturnFine(DateTime? loanEndDate)
        {
            var fine = 0.0;
            var calculator = new FineCalculator();
            if (loanEndDate.HasValue)
            {
                fine = calculator.CalculateLateReturnFine(DateTime.Now, loanEndDate.Value);
            }

            return fine;
        }

        private void AddBorrowerFine(Borrower borrower, double fineAmount)
        {
            if(fineAmount == 0.0)
            {
                return;
            }

            var fine = new Fine
            {
                Borrower = borrower,
                Amount = fineAmount
            };

            _fineRepository.AddFine(fine);
        }
    }
}
