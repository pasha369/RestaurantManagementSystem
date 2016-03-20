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
    public class HallManager : IDataManager<Hall>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(Hall item)
        {
            var current = _ctx.Halls.First(r => r.Id == item.Id);
            _ctx.Halls.Remove(current);
            _ctx.SaveChanges();
        }

        public void Add(Hall item)
        {
            _ctx.Halls.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(Hall item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public Hall GetById(Hall item)
        {
            return _ctx.Halls.FirstOrDefault(h => h.Id == item.Id);
        }

        public List<Hall> Get()
        {
            return _ctx.Halls
                .Include("Tables")
                .Include("Restaurant").ToList();
        }

        public Hall Get(int Id)
        {
            return _ctx.Halls.Include("Tables").FirstOrDefault(h => h.Id == Id);
        }

        IQueryable<Hall> IDataManager<Hall>.Get()
        {
            return _ctx.Halls.Include("Tables");
        }
    }
}
