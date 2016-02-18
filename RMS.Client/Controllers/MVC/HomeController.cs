using System.Web.Mvc;

namespace RMS.Client.Controllers.MVC
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Get home page.
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

    }
}
