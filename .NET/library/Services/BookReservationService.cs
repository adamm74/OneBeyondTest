using OneBeyondApi.DataAccess;
using OneBeyondApi.Model;

namespace OneBeyondApi.Services
{
    public class BookReservationService : IBookReservationService
    {
        private readonly IBookStockRepository _bookStockRepository;
        private readonly IBookReservationRepository _bookReservationRepository;
        private readonly IBorrowerRepository _borrowerRepository;

        private const int BookLoanDays = 21;    //number of days that books are loaned out for

        public BookReservationService(IBookStockRepository bookStockRepository, IBookReservationRepository bookReservationRepository, IBorrowerRepository borrowerRepository)
        {
            _bookStockRepository = bookStockRepository;
            _bookReservationRepository = bookReservationRepository;
            _borrowerRepository = borrowerRepository;
        }

        public string GetAvailableReservationDate(string isbn)
        {
            var bookStock = _bookStockRepository.GetBookStocks()
                .FirstOrDefault(x => x.Book.ISBN == isbn);

            if (bookStock == null)
            {
                return $"{isbn} could not be found.";
            }

            if(bookStock.LoanEndDate == null)
            {
                return $"This book is available immediately";
            }

            var responseString = GetBookReservedToDate(bookStock, isbn);
            return $"This book should be available from {responseString}"; ;
        }

        public string ReserveBook(string emailAddress, string isbn)
        {
            var borrower = _borrowerRepository.GetBorrowers().FirstOrDefault(x => x.EmailAddress == emailAddress);
            if(borrower == null)
            {
                return $"Email address has not been recognised";
            }

            var bookStock = _bookStockRepository.GetBookStocks().FirstOrDefault(x => x.Book.ISBN == isbn);
            if(bookStock == null)
            {
                return $"ISBN has not been recognised";
            }

            var reservation = new BookReservation
            {
                ReservedByID = borrower.Id,
                ISBN = bookStock.Book.ISBN,
                DateReserved = DateTime.Now
            };

            if (BorrowerHasBookReserved(borrower.Id, isbn))
            {
                return "You have already reserved this book";
            }

            _bookReservationRepository.AddBookReservation(reservation);

            return $"{bookStock.Book.Name} has been reserved";
        }

        private bool BorrowerHasBookReserved(Guid borrowerId, string isbn)
        {
            return _bookReservationRepository.GetReservedBooks()
                .Where(x => x.ISBN == isbn)
                .Any(x => x.ReservedByID == borrowerId);
        }

        public string GetBookReservedToDate(BookStock bookStock, string isbn)
        {
            var bookReservations = _bookReservationRepository.GetReservedBooks().Where(x => x.ISBN == isbn).ToList();
            var daysToAdd = BookLoanDays * bookReservations.Count();
            var availableFrom = bookStock.LoanEndDate!.Value.AddDays(daysToAdd);
            return availableFrom.ToString("dd, MMM, yy");
        }
    }
}
