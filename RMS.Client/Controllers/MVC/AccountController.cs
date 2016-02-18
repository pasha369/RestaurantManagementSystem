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

        public AccountController(IDataManager<UserInfo> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Get login view
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// If user exist redirect to profile
        /// </summary>
        /// <param name="login">View model with credentials</param>
        /// <returns>Profile page</returns>
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
                    if (user.Position == Role.Restaurateur)
                        return RedirectToAction("RestaurateurPage", "Profile");

                }
            }

            return View(login);
        }

        /// <summary>
        /// Logout
        /// </summary>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ProfilePage", "Profile");
        }

        /// <summary>
        /// Get register view.
        /// </summary>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register user in system.
        /// </summary>
        /// <param name="model">View model with user data.</param>
        /// <returns>If model not valid return errors else redirect to profile.</returns>
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map<UserInfo>(model);
                user.Position = Role.User;

                _userManager.Add(user);

                return RedirectToAction("ProfilePage", "Profile");
            }
            return View(model);
        }
    }
}
