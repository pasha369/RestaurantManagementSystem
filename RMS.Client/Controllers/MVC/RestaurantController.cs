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
        public IDataManager<Restaurant> _rstManager;
        

       
        public RestaurantController(IDataManager<Restaurant> rstManager)
        {
            _rstManager = rstManager;
        }

        //
        // GET: /Restaurant/

        public ActionResult RestaurantList()
        {
            return View();
        }
        public ActionResult RestaurantEdit(int Id)
        {
            var manager = new RestaurantManager();
            var restaurant = manager.GetById(Id);
            
            Mapper.CreateMap<Restaurant, RestaurantModel>()
                .ForMember(model => model.Phone,
                opt => opt.MapFrom(r => r.PhoneNumber.ToString()));
            var rstModel = Mapper.Map<RestaurantModel>(restaurant);

            return View(rstModel);
        }
        public ActionResult GetById(int Id)
        {
            var manager = new RestaurantManager();
            var restaurant = manager.GetById(Id);

            Mapper.CreateMap<Restaurant, RestaurantModel>()
                .ForMember(model => model.Phone,
                opt => opt.MapFrom(r => r.PhoneNumber.ToString()));
            var rstModel = Mapper.Map<RestaurantModel>(restaurant);

            return Json(rstModel, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult RestaurantEdit(RestaurantModel model)
        {
            if (ModelState.IsValid)
            {
                var manager = new RestaurantManager();
                var restaurant = manager.GetById(model.Id);

                restaurant.Name = model.Name;
                restaurant.Description = model.Description;
                restaurant.PhoneNumber = Convert.ToInt32(model.Phone);

                manager.Update(restaurant);
                return RedirectToAction("RestaurantDetail", "Restaurant", new { Id = model.Id });
            }
            return View(model);
        }

        public ActionResult RestaurantDetail(int Id)
        {
            var restaurant = _rstManager
                .GetAll()
                .FirstOrDefault(r => r.Id == Id);
            if (restaurant != null)
            {
                var model = new RestaurantModel();
                model.Id = restaurant.Id;
                model.Name = restaurant.Name;
                model.Description = restaurant.Description;
                model.PhotoUrl = restaurant.PhotoUrl;
                model.Phone = restaurant.PhoneNumber.ToString();
                model.CommentCount = restaurant.Reviews.Count();

                return View(model);
            }
            return View();
        }

        public ActionResult GetAll()
        {
            string strJSON = JsonConvert.SerializeObject(
                new RestaurantManager().GetAll(),
                Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return Json(strJSON, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BookTable(int Id)
        {
            BookingModel model = new BookingModel();
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

                var table = rstManager.GetAllTable(model.RestaurantId)
                    .FirstOrDefault();

                if (table != null)
                {
                    table.IsReserved = true;

                    var reservation = new Reservation();
                   
                    var login = System.Web.HttpContext.Current.User.Identity.Name;
                    reservation.User = userManager.GetAll().FirstOrDefault(u => u.Login == login);
                    reservation.PeopleCount = model.PeopleNum;
                    reservation.DateTime = model.Date;
                    reservation.To = model.Date;
                    reservation.SpecialRequest = model.Msg;
                    reservation.Table = table;
                    rsvManager.Add(reservation);

                    return RedirectToAction("RestaurantDetail", new { @Id = 1 });
                }
            }
            return View(model);
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
            var rstManager = new RestaurantManager();
            var favoriteManager = new FavoriteManager();
            var favorite = new Favorite();
            var login = System.Web.HttpContext.Current.User.Identity.Name;

            favorite.Restaurant = rstManager.GetById(Id);
            favorite.User = userManager.GetAll().FirstOrDefault(u => u.Login == login);
            favoriteManager.Add(favorite);

            return View();
        }
    }
}
