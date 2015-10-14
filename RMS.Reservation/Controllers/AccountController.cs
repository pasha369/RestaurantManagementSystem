using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Reservation.Models.ViewModel;
using RMS.Reservation.Models;

namespace RMS.Reservation.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

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

                    // Add ticket in cookie
                    var ticket = new FormsAuthenticationTicket(1, login.Username,
                        DateTime.Now, DateTime.Now.AddMinutes(2880), false, "", FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);

                    
                    return RedirectToRoute("Index");
                }
            }

            return View(login);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserInfo model)
        {
            if(ModelState.IsValid)
            {
                var manager = new UserManager();
                manager.Add(model);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View();
        }

    }
}
