using System;
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
        private IDataManager<City> _cityManager;

        public RestaurantController(IDataManager<Restaurant> rstManager, IDataManager<Cuisine> cuisineManager, IDataManager<City> cityManager)
        {
            _rstManager = rstManager;
            _cuisineManager = cuisineManager;
            _cityManager = cityManager;
        }

        /// <summary>
        /// Get RestaurantList page.
        /// </summary>
        /// <returns></returns>
        public ActionResult RestaurantList()
        {
            var restaurantLst = GetRestaurantList(null, String.Empty, String.Empty);
            return View(restaurantLst);
        }

        /// <summary>
        /// Get restaurant data.
        /// </summary>
        /// <param name="firstItemId"></param>
        /// <param name="city">ountry.</param>
        /// <param name="cuisine">Cuisine.</param>
        /// <returns>Json representation restaurant list</returns>
        [HttpPost]
        public ActionResult GetRestaurantPage(int? firstItemId, string city, string cuisine)
        {
            var restaurants = GetRestaurantList(firstItemId, city, cuisine);
            return Json(restaurants, JsonRequestBehavior.AllowGet);
        }

        private IQueryable<Restaurant> FilterByCity(IQueryable<Restaurant> restaurants, string city)
        {
            if (!string.IsNullOrEmpty(city))
            {
                restaurants = restaurants.Where(x => x.Adress.City.Name == city);
            }
            return restaurants;
        }

        private IQueryable<Restaurant> FilteByCuisine(IQueryable<Restaurant> restaurants, string cuisine)
        {
            if (!string.IsNullOrEmpty(cuisine))
            {
                restaurants = restaurants.Where(x => x.Cuisines.Any(c => c.Name == cuisine));
            }
            return restaurants;
        }

        private RestaurantLst GetRestaurantList(int? firstItemId, string city, string cuisine)
        {
            var perPage = 3;
            var restaurants = _rstManager.Get();
            restaurants = FilteByCuisine(restaurants, cuisine);
            restaurants = FilterByCity(restaurants, city);
            var restaurantLst = new RestaurantLst(_cuisineManager);
            restaurantLst.RestaurantCount = restaurants.Count();

            if (firstItemId.HasValue && restaurants.Count() > firstItemId.Value)
            {
                restaurants = restaurants
                                        .OrderBy(x => x.Id)
                                        .Skip(firstItemId.Value)
                                        .Take(perPage);
            }
            else
            {
                restaurants = restaurants.Take(perPage);
            }


            restaurantLst.CityList.AddRange(GetTopCity());
            restaurantLst.CuisineList.AddRange(GetTopCuisine());
            restaurantLst.Cities = _cityManager.Get().Select(x => x.Name).ToList();
            restaurantLst.Cuisines = _cuisineManager.Get().Select(x => x.Name).ToList();

            foreach (var restaurant in restaurants)
            {
                var model = Mapper.Map<RestaurantModel>(restaurant);
                model.IsExistFreeTable = CheckFreeTable(restaurant.Id);
                model.CommentCount = restaurant.Reviews?.Count() ?? 0;
                if (restaurant.Reviews?.Count > 0)
                {
                    model.Rating = restaurant.Reviews.Count > 0 ? restaurant.Reviews.Sum(r => r.Food) / restaurant.Reviews.Count : 0;
                }

                restaurantLst.RestaurantModels.Add(model);
            }
            return restaurantLst;
        }

        public ActionResult RestaurantListCountry(string country)
        {
            var items = _rstManager.Get().Where(r => (r.Adress.Country != null ? r.Adress.Country.Name : "") == country).ToList();
            var restaurantLst = new RestaurantLst();
            restaurantLst.CityList.AddRange(GetTopCity());

            foreach (var restaurant in items)
            {
                var model = Mapper.Map<RestaurantModel>(restaurant);
                restaurantLst.RestaurantModels.Add(model);
            }
            return View("RestaurantList", restaurantLst);
        }

        public ActionResult RestaurantListCuisine(string cuisine)
        {
            var items = _cuisineManager.Get()
                .FirstOrDefault(x => x.Name == cuisine)?.Restoraunts;
            var restaurantLst = new RestaurantLst();
            restaurantLst.CityList.AddRange(GetTopCity());
            restaurantLst.CuisineList.AddRange(GetTopCuisine());

            if (items != null)
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
                model.IsExistFreeTable = CheckFreeTable(restaurant.Id);
                if (restaurant.Adress != null)
                {
                    model.LongAddress = $"{restaurant.Adress.Street}, {restaurant.Adress.City.Name}";
                    model.CityName = restaurant.Adress.City.Name;
                }
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
            return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Check is free table exists.
        /// </summary>
        /// <param name="restaurantId">Restaurant id.</param>
        /// <returns>Boolean result.</returns>
        [HttpPost]
        public ActionResult IsFreeTableExist(int restaurantId)
        {
            return Json(new { isExist = CheckFreeTable(restaurantId) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFreeHours(DateTime date, int restaurantId)
        {
            // TODO: Method for get from date.
            var end = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0);
            var start = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            var times = new List<DateTime>();
            while (end > start)
            {
                start = start.AddMinutes(30);
                times.Add(start);
            }
            var timeList = new List<string>();

            foreach (var time in times)
            {
                var isExistFreeTable = _rstManager.Get(restaurantId)
                        .DinnerTables
                        .Where(x => !x.Reservations
                                        .Any(r => r.From.Year == time.Year 
                                                    && r.From.Month == time.Month 
                                                    && r.From.Day == time.Day 
                                                    ||( r.From > time && r.To < time)));
                if (!isExistFreeTable.Any())
                {
                    timeList.Add($"{time.Hour}:{time.Minute}");
                }
            }

            return Json(timeList, JsonRequestBehavior.AllowGet);
        }

        private bool CheckFreeTable(int restaurantId)
        {
            return _rstManager.Get(restaurantId)
                .DinnerTables
                .Any(x => x.Reservations.Count == 0);
        }

        private RestaurantModel GetModelById(int Id)
        {
            var restaurant = _rstManager.Get(Id);
            var model = Mapper.Map<RestaurantModel>(restaurant);

            return model;
        }

        private IEnumerable<string> GetTopCity()
        {
            var rst = _rstManager.Get();
            var countryLst = rst
                .GroupBy(r => r.Adress.City)
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
