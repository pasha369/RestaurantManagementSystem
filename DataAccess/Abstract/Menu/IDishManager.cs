using System.Linq;
using DataModel.Model;

namespace DataAccess.Abstract.Menu
{
    public interface IDishManager
    {
        void Add(Dish dish);
        void Delete(int dishId);
        void Update(Dish dish);
        IQueryable<Dish> Get();
        Dish Get(int id);

    }
}
