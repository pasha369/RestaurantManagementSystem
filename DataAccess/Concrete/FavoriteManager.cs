﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    public class FavoriteManager : IDataManager<Favorite>
    {
        private RestorauntDbContext _ctx = RestorauntDbContext.context;
        
        public void Delete(Favorite item)
        {
            var favorite = _ctx.Favorites
                .FirstOrDefault(f => f.Id == item.Id);
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

        public Favorite GetById(Favorite item)
        {
            throw new NotImplementedException();
        }
        public List<Restaurant> GetByUser(int userId)
        {
            var lstFavorite = _ctx.Favorites
                .Where(f => f.User.Id == userId)
                .Select(f => f.Restaurant)
                .ToList();
            return lstFavorite;
        } 
        public List<Favorite> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
