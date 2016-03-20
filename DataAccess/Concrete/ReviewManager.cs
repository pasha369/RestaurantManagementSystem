using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    public class ReviewManager : IDataManager<Review>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(Review item)
        {
            throw new NotImplementedException();
        }

        public void Add(Review item)
        {
            _ctx.Reviews.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(Review item)
        {
            throw new NotImplementedException();
        }

        public Review Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Review> Get()
        {
            return _ctx.Reviews;
        }

        public IQueryable<Review> GetByRestaurant(int RestaurantId)
        {
            return _ctx.Reviews.Where(r => r.Restaurant.Id == RestaurantId);
        }

    }
}
