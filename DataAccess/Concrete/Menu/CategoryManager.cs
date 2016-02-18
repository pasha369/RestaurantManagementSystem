using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract.Menu;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.Menu
{
    public class CategoryManager : ICategoryManager
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Add(int menuId, Category category)
        {
            var menu = _ctx.Menu.FirstOrDefault(m => m.Id == menuId);
            if (menu != null) 
                menu.Categories.Add(category);
            _ctx.SaveChanges();
        }

        public void Remove(int menuId, Category category)
        {
            var menu = _ctx.Menu.FirstOrDefault(m => m.Id == menuId);
            if (menu != null)
            {
                var curCategory = menu.Categories.FirstOrDefault(c => c.Id == category.Id);
                menu.Categories.Remove(curCategory);
            }
            _ctx.SaveChanges();
        }

        public void Update(int menuId, Category category)
        {
            var menu = _ctx.Menu.FirstOrDefault(m => m.Id == menuId);
            _ctx.Entry(category).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public Category GetById(int categoryId)
        {
            return _ctx.Categories.FirstOrDefault(c => c.Id == categoryId);
        }
    }
}
