using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Services;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Client.Filters;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.WebApi
{
    public class ReviewController : ApiController
    {
        [AjaxAuthorize]
        [WebMethod]
        public bool Save(ReviewModel model)
        {
            var reviewManager = new ReviewManager();
            
            var review = new Review();
            review.Restaurant = new RestaurantManager().GetById(model.RestaurantId);
            review.ReviewTime = DateTime.Now;
            review.Comment = model.Comment;
            review.Author = GetUserInfo();
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
                                     Author = r.Author.Name,
                                     PhotoUrl = r.Author.PhotoUrl,
                                     Food = r.Food,
                                     Comment = r.Comment,
                                     Service = r.Service,
                                     Stars = r.Stars,
                                 })
                .ToList();
            

            return lstReview;
        }

        private UserInfo GetUserInfo()
        {
            var userManager = new UserManager();

            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = userManager.GetUserByLogin(login);

            return user;
        }
    }
}
