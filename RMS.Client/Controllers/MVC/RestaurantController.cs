using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel.Model;
using Newtonsoft.Json;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.MVC
{
    public class RestaurantController : System.Web.Mvc.Controller
    {
        private IDataManager<Restaurant> _rstManager;
        private IDataManager<Cuisine> _cuisineManager;
        private IDataManager<Country> _countryManager; 

        public RestaurantController(IDataManager<Restaurant> rstManager, IDataManager<Cuisine> cuisineManager, IDataManager<Country> countryManager)
        {
            _rstManager = rstManager;
            _cuisineManager = cuisineManager;
            _countryManager = countryManager;
        }

        //
        // GET: /Restaurant/

        public ActionResult RestaurantList()
        {
            var items = _rstManager.GetAll();
            var restaurantLst = new RestaurantLst(_cuisineManager);
            restaurantLst.CountryList.AddRange(GetTopCountry());
            restaurantLst.CuisineList.AddRange(GetTopCuisine());
            foreach (var restaurant in items)
            {
                Mapper.CreateMap<Restaurant, RestaurantModel>();
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
            var items = _rstManager.GetAll().Where(r =>( r.Adress.Country != null?r.Adress.Country.Name:"") == country).ToList();
            var restaurantLst = new RestaurantLst();
            restaurantLst.CountryList.AddRange(GetTopCountry());

            foreach (var restaurant in items)
            {
                Mapper.CreateMap<Restaurant, RestaurantModel>();
                var model = Mapper.Map<RestaurantModel>(restaurant);
                restaurantLst.RestaurantModels.Add(model);
            }
            return View("RestaurantList", restaurantLst);
        }

        public ActionResult RestaurantListCuisine(string cuisine)
        {
            var items = _rstManager.GetAll()
                .Where(r => r.Cuisines.Count != 0 && r.Cuisines.Exists(c => c.Name== cuisine) )
                .ToList();
            var restaurantLst = new RestaurantLst();
            restaurantLst.CountryList.AddRange(GetTopCountry());
            restaurantLst.CuisineList.AddRange(GetTopCuisine());

            foreach (var restaurant in items)
            {
                Mapper.CreateMap<Restaurant, RestaurantModel>();
                var model = Mapper.Map<RestaurantModel>(restaurant);
                restaurantLst.RestaurantModels.Add(model);
            }

            return View("RestaurantList", restaurantLst);            
        }
        [HttpPost]
        public ActionResult RestaurantList(RestaurantLst model)
        {
            model.CountryList.AddRange(GetTopCountry());
            //var restaurantLst = new RestaurantLst(_cuisineManager);
            //var restaurants = _rstManager.GetAll().Where(
            //    r => r.Cuisines.FirstOrDefault(c => c.Name == model.Cuisine) != null
            //    ).ToList();
            //Mapper.CreateMap<Restaurant, RestaurantModel>();
            //model.RestaurantModels = Mapper.Map<List<Restaurant>, List<RestaurantModel>>(restaurants);

            //model.Cuisines = restaurantLst.Cuisines;
            return View(model);
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
                .Select(c => c.Name).AsEnumerable();

            return cuisineLst;
        } 
    }
}
