using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    public class DinnerTableManager : IDataManager<DinnerTable>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;


        public void Delete(DinnerTable item)
        {
            var tbl = _ctx.Tables.FirstOrDefault(t => t.Id == item.Id);
            _ctx.Tables.Remove(tbl);
            _ctx.SaveChanges();
        }

        public void Add(DinnerTable item)
        {
            var restaurant = _ctx.Restoraunts.FirstOrDefault(r => r.Id == item.Restaurant.Id);
            if (restaurant != null)
            {
                var hall = restaurant.Halls.FirstOrDefault(h => h.Id == item.Hall.Id);

                if (hall != null)
                {
                    if (hall.Tables == null)
                        hall.Tables = new List<DinnerTable>();
                    hall.Tables.Add(item);
                }
            }
            _ctx.SaveChanges();
        }

        public void Update(DinnerTable item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public DinnerTable GetById(DinnerTable item)
        {
            return _ctx.Tables.FirstOrDefault(r => r.Id == item.Id);
        }

        public List<DinnerTable> Get()
        {
            return _ctx.Tables.ToList();
        }

        public DinnerTable Get(int Id)
        {
            return _ctx.Tables.FirstOrDefault(t => t.Id == Id);
        }
    }
}
