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
