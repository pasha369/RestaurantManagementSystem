using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    /// <summary>
    /// Represents restaurant manager.
    /// </summary>
    public class RestaurantManager : IDataManager<Restaurant>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        /// <summary>
        /// Remove restaurant from db.
        /// </summary>
        /// <param name="item">Restaurant</param>
        public void Delete(Restaurant item)
        {
            var current = _ctx.Restoraunts.First(r => r.Id == item.Id);
            _ctx.Restoraunts.Remove(current);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Add restaurant to db.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Restaurant item)
        {
            item.Menu = new DataModel.Model.Menu();
            if (string.IsNullOrEmpty(item.PhotoUrl))
            {
                item.PhotoUrl = "~/Images/default/default-restaurant.png";
            }
            _ctx.Restoraunts.Add(item);
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Update restaurant data info.
        /// </summary>
        /// <param name="item">Restaurant data info.</param>
        public void Update(Restaurant item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        /// <summary>
        /// Get restaurant by id.
        /// </summary>
        /// <param name="id">Restaurant id.</param>
        /// <returns>Restaurant</returns>
        public Restaurant Get(int id)
        {
            return _ctx.Restoraunts.FirstOrDefault(r => r.Id == id);
        }

        /// <summary>
        /// Get restaurants.
        /// </summary>
        /// <returns>Restaurants</returns>
        public IQueryable<Restaurant> Get()
        {
            return _ctx.Restoraunts.Include("Halls")
                .Include(x => x.Cuisines)
                .Include(x => x.Adress);
        }
    }
}
