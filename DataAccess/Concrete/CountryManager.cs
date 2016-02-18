using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    public class CountryManager : IDataManager<Country>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(Country item)
        {
            var country = _ctx.Countries.FirstOrDefault(t => t.Id == item.Id);
            _ctx.Countries.Remove(country);
            _ctx.SaveChanges();
        }

        public void Add(Country item)
        {
            _ctx.Countries.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(Country item)
        {
            var country = _ctx.Countries.FirstOrDefault(c => c.Id == item.Id);
            country.Name = item.Name;

            _ctx.Entry(country).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public Country GetById(Country item)
        {
            return _ctx.Countries.FirstOrDefault(c => c.Id == item.Id);
        }

        public Country GetById(int id)
        {
            return _ctx.Countries.FirstOrDefault(c => c.Id == id);
        }

        public List<Country> GetAll()
        {
            return _ctx.Countries.ToList();
        }

    }
}
