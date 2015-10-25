using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Client.Models.View;

namespace RMS.Client.Controllers.MVC
{
    public class AccountController : Controller
    {
        private IDataManager<UserInfo> _userManager;

        public AccountController(IDataManager<UserInfo> userManager )
        {
            _userManager = userManager;
        }
            //
        // GET: /Account/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var user = new AuthRepository().Login(login.Username, login.Password);
                if (user != null)
                {
                    // Create ticket 
                    var ticket = new FormsAuthenticationTicket(1, login.Username,
                        DateTime.Now, DateTime.Now.AddMinutes(2880), 
                        false, 
                        Enum.GetName(typeof(Role), user.Position), 
                        FormsAuthentication.FormsCookiePath);
                    // Encode ticket
                    string hash = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    //Save ticket in coockie
                    Response.Cookies.Add(cookie);

                    if (user.Position == Role.User)
                        return RedirectToAction("UserPage", "Profile");
                    if(user.Position == Role.Restaurateur)
                        return RedirectToAction("RestaurateurPage", "Profile");

                }
            }

            return View(login);
        }

        public  ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                Mapper.CreateMap<RegisterModel, UserInfo>();
                var user = Mapper.Map<UserInfo>(model);

                user.Phone = model.Phone;
                user.Position = Role.User;

                _userManager.Add(user);

                return RedirectToAction("ProfilePage", "Profile");
            }
            return View(model);
        }
    }
}
