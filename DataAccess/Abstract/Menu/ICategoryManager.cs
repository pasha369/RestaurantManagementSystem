using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Model;

namespace DataAccess.Abstract.Menu
{
    public interface ICategoryManager
    {
        void Add(int menuId, Category category);
        void Remove(int menuId, Category category);
        void Update(int menuId, Category category);

        Category GetById(int categoryId);
    }
}
