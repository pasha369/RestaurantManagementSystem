using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    public class RestaurantManager : IDataManager<Restaurant>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(Restaurant item)
        {
            var current = _ctx.Restoraunts.First(r => r.Id == item.Id);
            _ctx.Restoraunts.Remove(current);
            _ctx.SaveChanges();
        }

        public void Add(Restaurant item)
        {
            _ctx.Restoraunts.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(Restaurant item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public Restaurant GetById(Restaurant item)
        {
            return _ctx.Restoraunts.FirstOrDefault(r => r.Id == item.Id);
        }
        public Restaurant GetById(int id)
        {
            return _ctx.Restoraunts.FirstOrDefault(r => r.Id == id);
        }

        public List<Restaurant> GetAll()
        {
            return _ctx.Restoraunts.Include("Halls")
                .Include("Cuisines")
                .ToList();
        }
        public List<DinnerTable> GetAllTable(int id)
        {

            var restaurant = _ctx.Restoraunts.FirstOrDefault(r => r.Id == id);
            var lstTables = new List<DinnerTable>();
            if (restaurant != null)
            {
                lstTables = restaurant.Halls.SelectMany(h => h.Tables).ToList();
            }
            return lstTables;
        }

 
    }
}
