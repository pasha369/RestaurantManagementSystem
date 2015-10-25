using System;
using System.Linq;
using System.Web.Mvc;
using DataAccess.Concrete;
using DataModel.Model;
using Newtonsoft.Json;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.MVC
{
    public class ProfileController : Controller
    {

        //
        // GET: /Profile/
        [Authorize(Roles = "User")]
        public ActionResult UserPage()
        {
            var userManager = new UserManager();
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = userManager.GetAll().FirstOrDefault(u => u.Login == login);

            if (user != null)
            {
                var model = new ProfileModel();
                model.Id = user.Id;
                model.Name = user.Name;
                model.PhotoUrl = user.PhotoUrl;
                model.Phone = user.Phone.ToString();
                model.Position = Enum.GetName(typeof(Role), user.Position);

                return View(model);
            }
            return View();
        }

        [Authorize(Roles = "Restaurateur")]
        public ActionResult RestaurateurPage()
        {
            var clientManager = new ClientManager();
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var clients = clientManager.GetAll();
            var client = clients.FirstOrDefault(u => u.UserInfo.Login == login);
            if (client != null)
            {
                var model = new ProfileModel();
                model.Id = client.Id;
                model.Name = client.UserInfo.Name;
                model.Phone = client.UserInfo.Phone.ToString();
                model.Position = Enum.GetName(typeof(Role), client.UserInfo.Position);
                model.RestaurantId = client.Restaurant.Id;

                return View(model);
            }
            return View();
        }

        public ActionResult ProfilePage()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var clients = new ClientManager().GetAll();
            var client = clients.FirstOrDefault(u => u.UserInfo.Login == login);
            if(client != null)
            {
                return RedirectToAction("RestaurateurPage");
            }
            return RedirectToAction("UserPage");
        }

        public ActionResult Favorites()
        {
            var userManager = new UserManager();
            var fvrManager = new FavoriteManager();
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = userManager.GetAll().FirstOrDefault(u => u.Login == login);

            var lstFavorites = fvrManager.GetByUser(user.Id);
            string strJSON = JsonConvert.SerializeObject(
                lstFavorites,
                Formatting.Indented,
            new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
                        
            return Json(strJSON, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveFavorite(Favorite item)
        {
            var fvrManager = new FavoriteManager();
            var user = GetUserByLogin();

            fvrManager.Delete(item.Id, user.Id);

            return View();
        }
        [HttpGet]
        public ActionResult GetUser()
        {
            var userManager = new UserManager();
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = userManager.GetAll().FirstOrDefault(u => u.Login == login);

            if (user != null)
            {
                var model = new ProfileModel();
                model.Id = user.Id;
                model.Name = user.Name;
                model.PhotoUrl = user.PhotoUrl;
                model.Password = user.Password;
                model.About = user.About;
                model.Email = user.Email;
                model.Facebook = user.Facebook;
                model.Skype = user.Skype;
                model.Phone = user.Phone;

                model.Position = Enum.GetName(typeof(Role), user.Position);

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(string.Empty);
        }

        private UserInfo GetUserByLogin()
        {
            var userManager = new UserManager();

            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = userManager.GetAll().FirstOrDefault(u => u.Login == login);

            return user;
        }
        [HttpPost]
        public ActionResult ProfileEdit(ProfileModel model)
        {
            if(ModelState.IsValid)
            {
                var userManager = new UserManager();
                var user = userManager.GetById(model.Id);

                user.Name = model.Name;
                user.About = model.About;

                user.Email = model.Email;
                user.Phone = model.Phone;
                user.Facebook = model.Facebook;
                user.Password = model.Password;
                user.Skype = model.Skype;

                userManager.Update(user);

            }
            return RedirectToAction("ProfilePage");
        }
    }
}
