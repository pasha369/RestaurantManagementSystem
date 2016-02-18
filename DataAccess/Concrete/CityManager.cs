using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    public class CityManager : IDataManager<City>
    {
        public RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(City item)
        {
            var city = _ctx.Cities.FirstOrDefault(t => t.Id == item.Id);
            _ctx.Cities.Remove(city);
            _ctx.SaveChanges();
        }

        public void Add(City item)
        {
            _ctx.Cities.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(City item)
        {
            var city = _ctx.Cities.FirstOrDefault(c => c.Id == item.Id);
            city.Name = item.Name;
            _ctx.SaveChanges();
        }

        public City GetById(City item)
        {
            return _ctx.Cities.FirstOrDefault(c => c.Id == item.Id);
        }

        public City GetById(int id)
        {
            return _ctx.Cities.FirstOrDefault(c => c.Id == id);
        }

        public List<City> GetAll()
        {
            return _ctx.Cities
                .Include("Country").ToList();
        }

    }
}
