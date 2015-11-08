using DataModel.Model;

namespace DataAccess.Abstract.Menu
{
    public interface IDishManager
    {
        void Add(Category category, Dish dish);
        void Delete(int dishId);
        void Update(Dish dish);

        void GetByCategory(Category category);
        Dish GetById(int dishId);
    }
}
