using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBookReservationRepository
    {
        public List<BookReservation> GetReservedBooks();

        Guid AddBookReservation(BookReservation reservation);

    }
}
