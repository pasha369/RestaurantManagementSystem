using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DataAccess.Abstract;
using DataModel.Model;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.MVC
{
    /// <summary>
    /// Represents home controller.
    /// </summary>
    public class HomeController : Controller
    {
        private IDataManager<Restaurant> _restaurantManager;

        /// <summary>
        /// Initialize home controller instance.
        /// </summary>
        /// <param name="restaurantManager"></param>
        public HomeController(IDataManager<Restaurant> restaurantManager)
        {
            _restaurantManager = restaurantManager;
        }

        /// <summary>
        /// Get home page.
        /// </summary>
        public ActionResult Index()
        {
            var homeModel = new HomeViewModel();
            homeModel.Restaurants = GetTopRestaurants();
            return View(homeModel);
        }

        private List<RestaurantModel> GetTopRestaurants()
        {
            var restaurants = _restaurantManager.Get()
                .OrderByDescending(x => x.Reviews.Select(r => (r.Ambience + r.Food + r.Service)/3).Sum())
                .Take(3);
            return Mapper.Map<List<RestaurantModel>>(restaurants);
        }
    }
}
