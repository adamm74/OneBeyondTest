using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BookReservationRepository : IBookReservationRepository
    {
        public BookReservationRepository()
        {
        }

        public List<BookReservation> GetReservedBooks()
        {
            using (var context = new LibraryContext())
            {
                var list = context.BookReservations
                    .ToList();
                return list;
            }
        }

        public Guid AddBookReservation(BookReservation reservation)
        {
            using (var context = new LibraryContext())
            {
                context.BookReservations.Add(reservation);
                context.SaveChanges();
                return reservation.Id;
            }
        }
    }
}
