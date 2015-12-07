using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel.Model;
using Newtonsoft.Json;
using PagedList;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.MVC
{
    public class RestaurantController : System.Web.Mvc.Controller
    {
        private IDataManager<Restaurant> _rstManager;
        private IDataManager<Cuisine> _cuisineManager;
        private readonly int RESTAURANT_ON_PAGE = 3;
        private readonly int FIRST_PAGE = 1;

        public RestaurantController(IDataManager<Restaurant> rstManager, IDataManager<Cuisine> cuisineManager)
        {
            _rstManager = rstManager;
            _cuisineManager = cuisineManager;
        }
        /// <summary>
        /// Restaurant list
        /// </summary>
        /// <param name="page">current page</param>
        /// <returns>RestaurantList view</returns>
        public ActionResult RestaurantList(int? page)
        {
            var items = _rstManager.GetAll();

            var restaurantLst = new RestaurantLst(_cuisineManager);

            restaurantLst.CountryList.AddRange(GetTopCountry());
            restaurantLst.CuisineList.AddRange(GetTopCuisine());
            var restaurants = new List<RestaurantModel>();
            foreach (var restaurant in items)
            {
                Mapper.CreateMap<Restaurant, RestaurantModel>();
                var model = Mapper.Map<RestaurantModel>(restaurant);

                if (restaurant.Reviews != null)
                {
                    model.CommentCount = restaurant.Reviews.Count();
                }
                if (restaurant.Reviews != null && restaurant.Reviews.Count > 0)
                {
                    model.Rating = restaurant.Reviews.Sum(r => r.Food) / restaurant.Reviews.Count;
                }
                restaurants.Add(model);
            }
            restaurantLst.RestaurantModels = restaurants.ToPagedList(page ?? FIRST_PAGE, RESTAURANT_ON_PAGE);
            return View(restaurantLst);
        }
        /// <summary>
        /// Restaurant list by country
        /// </summary>
        /// <param name="country">choosen country</param>
        /// <param name="page">current page</param>
        /// <returns></returns>
        public ActionResult RestaurantListCountry(string country, int? page)
        {
            var items = _rstManager.GetAll().Where(r => (r.Adress.Country != null ? r.Adress.Country.Name : "") == country).ToList();
            var restaurantLst = new RestaurantLst();
            restaurantLst.CountryList.AddRange(GetTopCountry());
            var restaurants = new List<RestaurantModel>();
            foreach (var restaurant in items)
            {
                Mapper.CreateMap<Restaurant, RestaurantModel>();
                var model = Mapper.Map<RestaurantModel>(restaurant);
                restaurants.Add(model);
            }
            restaurantLst.RestaurantModels = restaurants.ToPagedList(page ?? FIRST_PAGE, RESTAURANT_ON_PAGE);
            return View("RestaurantList", restaurantLst);
        }
        /// <summary>
        /// Restaurant list by cuisine
        /// </summary>
        /// <param name="cuisine">choosen cuisine</param>
        /// <param name="page">current page</param>
        /// <returns></returns>
        public ActionResult RestaurantListCuisine(string cuisine, int? page)
        {
            var items = _rstManager.GetAll()
                .Where(r => r.Cuisines.Count != 0 && r.Cuisines.Exists(c => c.Name == cuisine))
                .ToList();
            var restaurantLst = new RestaurantLst();
            restaurantLst.CountryList.AddRange(GetTopCountry());
            restaurantLst.CuisineList.AddRange(GetTopCuisine());
            var restaurants = new List<RestaurantModel>();
            foreach (var restaurant in items)
            {
                Mapper.CreateMap<Restaurant, RestaurantModel>();
                var model = Mapper.Map<RestaurantModel>(restaurant);
                restaurants.Add(model);
            }
            restaurantLst.RestaurantModels = restaurants.ToPagedList(page ?? FIRST_PAGE, RESTAURANT_ON_PAGE);
            return View("RestaurantList", restaurantLst);
        }
        /// <summary>
        /// Get restaurant model for edit
        /// </summary>
        /// <param name="Id">restaurant Id</param>
        /// <returns>RestaurantModel</returns>
        public ActionResult RestaurantEdit(int Id)
        {
            var rstModel = GetModelById(Id);
            return Json(rstModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get model by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetById(int Id)
        {
            var rstModel = GetModelById(Id);
            return Json(rstModel, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Edit restaurant
        /// </summary>
        /// <param name="model">editable restaurant</param>
        [HttpPost]
        public void RestaurantEdit(RestaurantModel model)
        {
            if (ModelState.IsValid)
            {

                var restaurant = _rstManager.GetById(model.Id);

                Mapper.CreateMap<RestaurantModel, Restaurant>();
                Mapper.Map<RestaurantModel, Restaurant>(model, restaurant);

                _rstManager.Update(restaurant);
            }
        }
        /// <summary>
        /// Get restaurant detail
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>RestaurantModel</returns>
        public ActionResult RestaurantDetail(int Id)
        {
            var restaurant = _rstManager.GetById(Id);

            if (restaurant != null)
            {
                Mapper.CreateMap<Restaurant, RestaurantModel>();
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

        public ActionResult GetAll()
        {
            string strJSON = JsonConvert.SerializeObject(
                _rstManager.GetAll(),
                Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return Json(strJSON, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRestaurantByClient()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var clientManager = new ClientManager();

            var client = clientManager.GetAll().FirstOrDefault(c => c.UserInfo.Login == login);

            string restaurant = JsonConvert.SerializeObject(
                client.Restaurant,
                Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return Json(restaurant, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void AddFavorite(int Id)
        {
            var userManager = new UserManager();
            var favoriteManager = new FavoriteManager();

            var favorite = new Favorite();
            var login = System.Web.HttpContext.Current.User.Identity.Name;

            favorite.Restaurant = _rstManager.GetById(Id);
            favorite.User = userManager.GetAll().FirstOrDefault(u => u.Login == login);
            favoriteManager.Add(favorite);
        }

        private RestaurantModel GetModelById(int Id)
        {
            var restaurant = _rstManager.GetById(Id);

            Mapper.CreateMap<Restaurant, RestaurantModel>();
            var model = Mapper.Map<RestaurantModel>(restaurant);

            return model;
        }

        private IEnumerable<string> GetTopCountry()
        {
            var rst = _rstManager.GetAll();
            var countryLst = rst
                .GroupBy(r => r.Adress.Country)
                .OrderByDescending(country => country.Count())
                .Select(gr => new { Name = gr.Key.Name, Count = gr.Count() })
                .Select(c => c.Name).Take(3);
            return countryLst;
        }

        private IEnumerable<string> GetTopCuisine()
        {
            var cuisineLst = _cuisineManager.GetAll()
                .OrderByDescending(c => c.Restoraunts.Count())
                .Select(c => c.Name)
                .Take(3).AsEnumerable();

            return cuisineLst;
        }
    }
}
