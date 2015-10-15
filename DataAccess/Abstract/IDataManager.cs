using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Abstract
{
    public interface IDataManager<T>
    {
        void Delete(T item);
        void Add(T item);
        void Update(T item);
        T GetById(T item);
        T GetById(int Id);
        List<T> GetAll();
    }
}
