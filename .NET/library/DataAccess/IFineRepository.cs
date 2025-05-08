using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public interface IFineRepository
    {
        Guid AddFine(Fine fine);

        public List<Fine> GetFines();
    }
}