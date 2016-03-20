using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract.Menu;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.Menu
{
    /// <summary>
    /// Represents dish manager
    /// </summary>
    public class DishManager: IDishManager
    {
        public RestorauntDbContext _ctx = new ContextManager().Context;

        /// <summary>
        /// Add dish to db.
        /// </summary>
        /// <param name="dish">Dish entity</param>
        public void Add(Dish dish)
        {
            _ctx.Dishes.Add(dish);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Remove dish.
        /// </summary>
        /// <param name="dishId">Dish id</param>
        public void Delete(int dishId)
        {
            var dish = _ctx.Dishes.FirstOrDefault(d => d.Id == dishId);
            _ctx.Dishes.Remove(dish);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Update dish data and save changes.
        /// </summary>
        /// <param name="dish">Dish entity</param>
        public void Update(Dish dish)
        {
            _ctx.Entry(dish).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Get query for all dishes.
        /// </summary>
        /// <returns>Dishes query</returns>
        public IQueryable<Dish> Get()
        {
            return _ctx.Dishes;
        }

        /// <summary>
        /// Get dish by id
        /// </summary>
        /// <param name="id">Dish id</param>
        /// <returns>Dish</returns>
        public Dish Get(int id)
        {
            return _ctx.Dishes.Find(id);
        }
    }
}
