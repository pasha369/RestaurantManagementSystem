using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract.Menu;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.Menu
{
    public class DishManager: IDishManager
    {
        public RestorauntDbContext _ctx = new ContextManager().Context;

        public void Add(Category category, Dish dish)
        {
            category.Dishes.Add(dish);
            _ctx.SaveChanges();
        }

        public void Delete(int dishId)
        {
            var dish = _ctx.Dishes.FirstOrDefault(d => d.Id == dishId);
            _ctx.Dishes.Remove(dish);
            _ctx.SaveChanges();
        }

        public void Update(Dish dish)
        {
            _ctx.Entry(dish).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void GetByCategory(Category category)
        {
            // TODO: decide whether or no
        }

        public Dish GetById(int dishId)
        {
            return _ctx.Dishes.FirstOrDefault(d => d.Id == dishId);
        }
    }
}
