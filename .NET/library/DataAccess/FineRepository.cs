using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class FineRepository : IFineRepository
    {
        public FineRepository()
        {
        }

        public List<Fine> GetFines()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Fines.Include(x => x.Borrower)
                    .ToList();
                return list;
            }
        }

        public Guid AddFine(Fine fine)
        {
            using (var context = new LibraryContext())
            {
                context.Fines.Update(fine);
                context.SaveChanges();
                return fine.Id;
            }
        }


    }
}
