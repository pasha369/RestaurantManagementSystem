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
    public class MessageManager : IMessageManager
    {
        static RestorauntDbContext _ctx = RestorauntDbContext.context;

        public void AddReview(Review review)
        {
            _ctx.Reviews.Add(review);
            _ctx.SaveChanges();
        }
        public void ApplyReview(Review review)
        {

            var rvw = _ctx.Reviews.FirstOrDefault(r => r.Id == review.Id);
            if (rvw != null)
                rvw.Status = Status.Applied;

            _ctx.SaveChanges();

        }

        public void CheckSpam(Review review)
        {

            var rvw = _ctx.Reviews.FirstOrDefault(r => r.Id == review.Id);
            if (rvw != null) 
                rvw.Status = Status.Spam;

            _ctx.SaveChanges();
        }

        public  List<Review> GetAllReview()
        {
            return  _ctx.Reviews.Where(r => r.Status == Status.Unknown).ToList();
        }
    }
}
