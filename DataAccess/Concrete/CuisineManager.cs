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
    public class CuisineManager : IDataManager<Cuisine>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(Cuisine item)
        {
            var current = _ctx.Cuisines.First(r => r.Id == item.Id);
            _ctx.Cuisines.Remove(current);
            _ctx.SaveChanges();
        }

        public void Add(Cuisine item)
        {
            _ctx.Cuisines.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(Cuisine item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public Cuisine GetById(Cuisine item)
        {
            return _ctx.Cuisines.FirstOrDefault(c => c.Id == item.Id);
        }

        public Cuisine Get(int id)
        {
            return _ctx.Cuisines.FirstOrDefault(c => c.Id == id);
        }

        public List<Cuisine> Get()
        {
            return _ctx.Cuisines.ToList();
        }
    }
}
