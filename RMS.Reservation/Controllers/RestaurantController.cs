using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Reservation.Models.ViewModel;

namespace RMS.Reservation.Controllers
{
    public class RestaurantController : Controller
    {
        //
        // GET: /Restaurant/
        
        public ActionResult ReserveTable(int geusts, DateTime time, int restaurantId)
        {
            var rstManager = new RestaurantManager();
            var userManager = new UserDataManager();

            var reservationModel = new ReserveModel();
            reservationModel.Restaurant = rstManager.GetById(restaurantId);
            reservationModel.Geusts = geusts;
            reservationModel.From = time;

            return View(reservationModel);
        }

        [HttpPost]
        public ActionResult ReserveTable(ReserveModel model)
        {
            var rstManager = new RestaurantManager();
            var userManager = new UserDataManager();

            return View();
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var manager = new RestaurantManager();
            var restaurant = manager.GetById(id);

            var detailModel = new DetailModel();
            detailModel.Restaurant = restaurant;
            detailModel.RestorauntId = id;
            return View(detailModel);
        }

        [HttpPost]
        public ActionResult Detail(int id, DetailModel model)
        {
            if(ModelState.IsValid)
            {
                var manager = new RestaurantManager();
                return RedirectToAction("ReserveTable", new{geusts = model.Guests, 
                    time = model.Time, restaurantId= id});
    
            }
            
            return View(model);
        }

    }
}
