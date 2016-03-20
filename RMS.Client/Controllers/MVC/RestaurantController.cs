using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.User;
using DataModel.Model;
using Newtonsoft.Json;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.MVC
{
    /// <summary>
    /// Represents restaurant controller.
    /// </summary>
    public class RestaurantController : System.Web.Mvc.Controller
    {
        private IDataManager<Restaurant> _rstManager;
        private IDataManager<Cuisine> _cuisineManager;

        public RestaurantController(IDataManager<Restaurant> rstManager, IDataManager<Cuisine> cuisineManager)
        {
            _rstManager = rstManager;
            _cuisineManager = cuisineManager;
        }

        public ActionResult RestaurantList()
        {
            var items = _rstManager.Get();
            var restaurantLst = new RestaurantLst(_cuisineManager);
            restaurantLst.CountryList.AddRange(GetTopCountry());
            restaurantLst.CuisineList.AddRange(GetTopCuisine());
            foreach (var restaurant in items)
            {
                var model = Mapper.Map<RestaurantModel>(restaurant);

                if (restaurant.Reviews != null)
                {
                    model.CommentCount = restaurant.Reviews.Count();
                }
                if (restaurant.Reviews.Count > 0)
                {
                    model.Rating = restaurant.Reviews.Sum(r => r.Food) / restaurant.Reviews.Count;
                }

                restaurantLst.RestaurantModels.Add(model);
            }
            return View(restaurantLst);
        }
        public ActionResult RestaurantListCountry(string country)
        {
            var items = _rstManager.Get().Where(r => (r.Adress.Country != null ? r.Adress.Country.Name : "") == country).ToList();
            var restaurantLst = new RestaurantLst();
            restaurantLst.CountryList.AddRange(GetTopCountry());

            foreach (var restaurant in items)
            {
                var model = Mapper.Map<RestaurantModel>(restaurant);
                restaurantLst.RestaurantModels.Add(model);
            }
            return View("RestaurantList", restaurantLst);
        }

        public ActionResult RestaurantListCuisine(string cuisine)
        {
            var items = _rstManager.Get()
                .Where(r => r.Cuisines.Count != 0 && r.Cuisines.Exists(c => c.Name == cuisine))
                .ToList();
            var restaurantLst = new RestaurantLst();
            restaurantLst.CountryList.AddRange(GetTopCountry());
            restaurantLst.CuisineList.AddRange(GetTopCuisine());

            foreach (var restaurant in items)
            {
                var model = Mapper.Map<RestaurantModel>(restaurant);
                restaurantLst.RestaurantModels.Add(model);
            }

            return View("RestaurantList", restaurantLst);
        }

        public ActionResult RestaurantEdit(int Id)
        {
            var rstModel = GetModelById(Id);
            return Json(rstModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetById(int Id)
        {
            var rstModel = GetModelById(Id);
            return Json(rstModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Restaurant edit.
        /// </summary>
        /// <param name="model">Restaurant model.</param>
        [HttpPost]
        public void RestaurantEdit(RestaurantModel model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = _rstManager.Get(model.Id);
                Mapper.Map<RestaurantModel, Restaurant>(model, restaurant);

                _rstManager.Update(restaurant);
            }
        }

        /// <summary>
        /// Get restaurant detail.
        /// </summary>
        /// <param name="Id">Restaurant id.</param>
        /// <returns>view</returns>
        public ActionResult RestaurantDetail(int Id)
        {
            var restaurant = _rstManager.Get(Id);

            if (restaurant != null)
            {
                var model = Mapper.Map<RestaurantModel>(restaurant);

                model.CommentCount = restaurant.Reviews.Count();
                if (restaurant.Reviews.Count > 0)
                {
                    model.Rating = restaurant.Reviews.Sum(r => r.Food) / restaurant.Reviews.Count;
                }

                return View(model);
            }
            return View();
        }

        /// <summary>
        /// Get all reservation.
        /// </summary>
        /// <returns>json reservation.</returns>
        public ActionResult GetAll()
        {
            string strJSON = JsonConvert.SerializeObject(
                _rstManager.Get(),
                Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return Json(strJSON, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get resraurant by client.
        /// </summary>
        /// <returns>client restaurant.</returns>
        public ActionResult GetRestaurantByClient()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var clientManager = new ClientManager();

            var client = clientManager.Get().FirstOrDefault(c => c.UserInfo.Login == login);

            string restaurant = JsonConvert.SerializeObject(
                client.Restaurant,
                Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return Json(restaurant, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add restaurant to favorite list
        /// </summary>
        /// <param name="Id">Restaurant id.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddFavorite(int Id)
        {
            var userManager = new UserManager();
            var favoriteManager = new FavoriteManager();

            var favorite = new Favorite();
            var login = System.Web.HttpContext.Current.User.Identity.Name;

            favorite.Restaurant = _rstManager.Get(Id);
            favorite.User = userManager.Get().FirstOrDefault(u => u.Login == login);
            favoriteManager.Add(favorite);
            return Json(new {result = "success"}, JsonRequestBehavior.AllowGet);
        }

        private RestaurantModel GetModelById(int Id)
        {
            var restaurant = _rstManager.Get(Id);
            var model = Mapper.Map<RestaurantModel>(restaurant);

            return model;
        }

        private IEnumerable<string> GetTopCountry()
        {
            var rst = _rstManager.Get();
            var countryLst = rst
                .GroupBy(r => r.Adress.Country)
                .OrderByDescending(country => country.Count())
                .Select(gr => new { Name = gr.Key.Name, Count = gr.Count() })
                .Select(c => c.Name).Take(3);
            return countryLst;
        }

        private IEnumerable<string> GetTopCuisine()
        {
            var cuisineLst = _cuisineManager.Get()
                .OrderByDescending(c => c.Restoraunts.Count())
                .Select(c => c.Name)
                .Take(3).AsEnumerable();

            return cuisineLst;
        }
    }
}
