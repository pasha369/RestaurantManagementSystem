using System.Linq;

namespace DataAccess.Abstract
{
    public interface IDataManager<T>
    {
        void Delete(T item);
        void Add(T item);
        void Update(T item);
        T Get(int Id);
        IQueryable<T> Get();
    }
}
