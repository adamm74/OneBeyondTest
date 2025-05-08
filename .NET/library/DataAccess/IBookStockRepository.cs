using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IBookStockRepository
    {
        public List<BookStock> GetBookStocks();

    }
}
