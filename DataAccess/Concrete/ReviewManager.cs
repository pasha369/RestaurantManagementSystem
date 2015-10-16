using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    public class ReviewManager : IDataManager<Review>
    {
        private static RestorauntDbContext _ctx = RestorauntDbContext.context;

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

        public Review GetById(Review item)
        {
            throw new NotImplementedException();
        }

        public Review GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Review> GetAll()
        {
            return _ctx.Reviews.ToList();
        }
        public List<Review> GetByRestaurant(int RestaurantId)
        {
            return _ctx.Reviews.Where(r => r.Restaurant.Id == RestaurantId).ToList();
        }
    }
}
