using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Services;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.WebApi
{
    public class ReviewController : ApiController
    {
        [WebMethod]
        public bool Save(ReviewModel model)
        {
            var reviewManager = new ReviewManager();
            
            var review = new Review();
            review.Restaurant = new RestaurantManager().GetById(model.RestaurantId);
            review.Author = model.Author;
            review.ReviewTime = DateTime.Now;
            review.Comment = model.Comment;
            review.Author = model.Author;
            review.Food = model.Food;
            review.Ambience = model.Ambience;
            review.Service = model.Service;
            
            reviewManager.Add(review);

            return true;
        }

        [HttpGet]
        public List<ReviewModel> GetAll(int Id)
        {
            
            var reviewManager = new ReviewManager();
            var lstReview = reviewManager.GetByRestaurant(Id)
                .Select(r => new ReviewModel()
                                 {
                                     Ambience = r.Ambience,
                                     Author = r.Author,
                                     Food = r.Food,
                                     Comment = r.Comment,
                                     Service = r.Service,
                                     Stars = r.Stars,
                                 })
                .ToList();
            

            return lstReview;
        }
    }
}
