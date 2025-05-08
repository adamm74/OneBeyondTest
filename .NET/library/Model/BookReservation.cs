using System.ComponentModel.DataAnnotations.Schema;

namespace OneBeyondApi.Model
{
    public class BookReservation
    {
        public Guid Id { get; set; }
        [NotMapped]
        public Guid ReservedByID { get; set; }
        [NotMapped]
        public string ISBN { get; set; }
        public DateTime DateReserved { get; set; }
    }
}
