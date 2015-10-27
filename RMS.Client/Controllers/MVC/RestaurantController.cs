using System;
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

        public RestaurantController(IDataManager<Restaurant> rstManager)
        {
            _rstManager = rstManager;
        }

        //
        // GET: /Restaurant/

        public ActionResult RestaurantList()
        {
            var items = _rstManager.GetAll();
            var restaurantLst = new RestaurantLst();

            foreach (var restaurant in items)
            {
                Mapper.CreateMap<Restaurant, RestaurantModel>();
                var model = Mapper.Map<RestaurantModel>(restaurant);

                if(restaurant.Reviews != null)
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
        public ActionResult RestaurantEdit(int Id)
        {

            var rstModel = GetModelById(Id);

            return View(rstModel);
        }
        public ActionResult GetById(int Id)
        {

            var rstModel = GetModelById(Id);

            return Json(rstModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RestaurantEdit(RestaurantModel model)
        {
            if (ModelState.IsValid)
            {

                var restaurant = _rstManager.GetById(model.Id);

                Mapper.CreateMap<RestaurantModel, Restaurant>();
                Mapper.Map<RestaurantModel, Restaurant>(model, restaurant);

                _rstManager.Update(restaurant);
                return RedirectToAction("RestaurantDetail", "Restaurant", new { Id = model.Id });
            }
            return View(model);
        }

        public ActionResult RestaurantDetail(int Id)
        {
            var restaurant = _rstManager.GetById(Id);

            if (restaurant != null)
            {
                Mapper.CreateMap<Restaurant, RestaurantModel>();
                var model = Mapper.Map<RestaurantModel>(restaurant);

                model.CommentCount = restaurant.Reviews.Count();
                if(restaurant.Reviews.Count > 0)
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
        public ActionResult BookTable(int Id)
        {
            var model = new BookingModel();
            model.RestaurantId = Id;
            return View(model);
        }

        [HttpPost]
        public ActionResult BookTable(BookingModel model)
        {
            if (ModelState.IsValid)
            {
                var rstManager = new RestaurantManager();
                var rsvManager = new ReservationManager();
                var userManager = new UserManager();
                var tblManager = new DinnerTableManager();
                var table = rstManager.GetAllTable(model.RestaurantId)
                    .FirstOrDefault();

                if (table != null)
                {
                    tblManager.Update(table);

                    var reservation = new Reservation();

                    var login = System.Web.HttpContext.Current.User.Identity.Name;
                    reservation.User = userManager.GetAll().FirstOrDefault(u => u.Login == login);
                    reservation.PeopleCount = model.PeopleNum;
                    reservation.From = model.From;
                    reservation.To = model.To;
                    reservation.SpecialRequest = model.Msg;
                    reservation.Table = table;
                    rsvManager.Add(reservation);

                    return Json(RedirectToAction("RestaurantDetail", new { @Id = 1 }));
                }
            }
            return Json("");
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
        public ActionResult AddFavorite(int Id)
        {
            var userManager = new UserManager();
            var favoriteManager = new FavoriteManager();

            var favorite = new Favorite();
            var login = System.Web.HttpContext.Current.User.Identity.Name;

            favorite.Restaurant = _rstManager.GetById(Id);
            favorite.User = userManager.GetAll().FirstOrDefault(u => u.Login == login);
            favoriteManager.Add(favorite);

            return View();
        }

        private RestaurantModel GetModelById(int Id)
        {
            var restaurant = _rstManager.GetById(Id);

            Mapper.CreateMap<Restaurant, RestaurantModel>();
            var model = Mapper.Map<RestaurantModel>(restaurant);

            return model;
        }
    }
}
