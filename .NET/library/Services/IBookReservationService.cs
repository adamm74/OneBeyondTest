using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBookReservationService
    {
        public string GetAvailableReservationDate(string isbn);

        public string ReserveBook(string emailAddress, string isbn);

    }
}
