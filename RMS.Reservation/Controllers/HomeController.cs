using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using DataAccess.Concrete;
using Newtonsoft.Json;

namespace RMS.Reservation.Controllers
{

    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            var rManager = new RestaurantManager();
            var lstRst = rManager.GetAll();

            return View(lstRst);
        }
        public ActionResult GetAll()
        {
            string strJSON = JsonConvert.SerializeObject(new RestaurantManager().GetAll(), Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return Json(strJSON, JsonRequestBehavior.AllowGet);
        }

    }
}
