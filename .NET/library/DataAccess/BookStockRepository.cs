using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BookStockRepository : IBookStockRepository
    {
        public BookStockRepository()
        {
        }

        public List<BookStock> GetBookStocks()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue
                    .ToList();
                return list;
            }
        }

    }
}
