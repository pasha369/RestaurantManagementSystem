using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using DataAccess.Abstract;
using DataModel.Model;
using RMS.Reservation.Core.Abstract;

namespace RMS.Reservation.Core.Concrete
{
    public class CustomAuthentication : IAuthentication
    {
        public HttpContext HttpContext { get; set; }
        public IPrincipal CurrentUser { get; set; }
        public IAuthRepository AuthRepository { set; get; }

        public UserInfo Login(string login, string password, bool rememberMe)
        {
            var user = AuthRepository.Login(login, password);
            if(user != null)
            {
                CreateCookie(login, rememberMe);
            }
            return user;
        }

        private void CreateCookie(string login, bool rememberMe)
        {
            var ticket = new FormsAuthenticationTicket(1, login, DateTime.Now,
                                                       DateTime.Now.Add(FormsAuthentication.Timeout),
                                                       rememberMe, string.Empty, FormsAuthentication.FormsCookiePath);
            var encTicket = FormsAuthentication.Encrypt(ticket);

            var AuthCookie = new HttpCookie("__AUTH_COOKIE")
                                 {
                                     Value = encTicket,
                                     Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
                                 };
            HttpContext.Response.SetCookie(AuthCookie);
        }

        public UserInfo Login(string login)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}