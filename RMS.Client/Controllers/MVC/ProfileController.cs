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
    public class ProfileController : Controller
    {
        private IDataManager<UserInfo> _userManager;
        private IDataManager<ClientInfo> _clientManager;

        public ProfileController(IDataManager<UserInfo> userManager, IDataManager<ClientInfo> clientManager)
        {
            _userManager = userManager;
            _clientManager = clientManager;
        }

        //
        // GET: /Profile/
        [Authorize(Roles = "User")]
        public ActionResult UserPage()
        {
            var user = GetUserByLogin();
            if (user != null)
            {
                var model = Mapper.Map<ProfileModel>(user);
                model.Position = Enum.GetName(typeof(Role), user.Position);

                return View(model);
            }
            return View();
        }

        [Authorize(Roles = "Restaurateur")]
        public ActionResult RestaurateurPage()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var clients = _clientManager.Get();
            var client = clients.FirstOrDefault(u => u.UserInfo.Login == login);

            if (client != null)
            {
                var model = new ProfileModel();
                model.Id = client.Id;
                model.Name = client.UserInfo.Name;
                model.Phone = client.UserInfo.Phone;
                model.Position = Enum.GetName(typeof(Role), client.UserInfo.Position);
                model.RestaurantId = client.Restaurant.Id;

                return View(model);
            }
            return View();
        }

        public ActionResult ProfilePage()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var clients = _clientManager.Get();
            var client = clients.FirstOrDefault(u => u.UserInfo.Login == login);
            if (client != null)
            {
                return RedirectToAction("RestaurateurPage");
            }
            return RedirectToAction("UserPage");
        }

        public ActionResult Favorites()
        {
            var fvrManager = new FavoriteManager();
            var user = GetUserByLogin();

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
            var user = GetUserByLogin();

            if (user != null)
            {
                Mapper.CreateMap<UserInfo, ProfileModel>();
                var model = Mapper.Map<ProfileModel>(user);
                model.Position = Enum.GetName(typeof(Role), user.Position);

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json(string.Empty);
        }

        [HttpPost]
        public ActionResult ProfileEdit(ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Get(model.Id);
                Mapper.Map<ProfileModel, UserInfo>(model, user);

                _userManager.Update(user);

            }
            return RedirectToAction("ProfilePage");
        }

        private UserInfo GetUserByLogin()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _userManager.Get().FirstOrDefault(u => u.Login == login);

            return user;
        }
    }
}
