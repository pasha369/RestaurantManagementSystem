using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    public class FavoriteManager : IDataManager<Favorite>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(Favorite item)
        {
            var favorite = _ctx.Favorites.FirstOrDefault(f => f.Id == item.Id);
            _ctx.Favorites.Remove(favorite);
            _ctx.SaveChanges();
        }

        public void Add(Favorite item)
        {
            _ctx.Favorites.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(Favorite item)
        {
            throw new NotImplementedException();
        }

        public Favorite Get(int Id)
        {
            return _ctx.Favorites.FirstOrDefault(f => f.Id == Id);
        }

        public IQueryable<Favorite> Get()
        {
            return _ctx.Favorites;
        }

        public List<Restaurant> GetByUser(int userId)
        {
            var lstFavorite = _ctx.Favorites
                .Where(f => f.User.Id == userId)
                .Select(f => f.Restaurant)
                .ToList();
            return lstFavorite;
        }

        public void Delete(int item, int id)
        {
            var favorite = _ctx.Favorites.FirstOrDefault(f => f.User.Id == id && f.Restaurant.Id == item);
            _ctx.Favorites.Remove(favorite);
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
