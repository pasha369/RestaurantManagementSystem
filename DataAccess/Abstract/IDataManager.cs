using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IDataManager<T>
    {
        void Delete(T item);
        void Add(T item);
        void Update(T item);
        T GetById(T item);
        T Get(int Id);
        List<T> Get();
    }
}
